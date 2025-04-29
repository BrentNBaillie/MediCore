using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Services;
using MediCore_API.Models.DTOs;
using MediCore_API.Models.DTOs.DTO_Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public BillController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<BillDTO>>> GetAllBills()
		{
			var bills = await context.Bills.ToListAsync();
			if (!bills.Any()) return NotFound("No Bills Found");

			return Ok(bills.Select(b => mapper.Map<Bill, BillDTO>(b)).ToList());
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<BillDTO>> GetBill([FromRoute] Guid id)
		{
			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
			if (bill is null) return NotFound("Bill Not Found");
			return Ok(mapper.Map<Bill, BillDTO>(bill));
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<BillDTO>>> GetPatientBills([FromRoute] Guid id)
		{
			if (!await context.Patients.AnyAsync(p => p.Id == id)) return NotFound("Patient Not Found");
			var bills = await context.Bills.Include(b => b.Patient).Include(b => b.Prescriptions).Where(b => b.PatientId == id).ToListAsync();
			if (!bills.Any()) return NotFound("No Bills Found");

			foreach (Bill bill in bills)
			{
				bill.Amount = bill.Prescriptions.Sum(p => p.Quantity * p.Medicine.Price);
			}

			return Ok(bills.Select(b => mapper.Map<Bill, BillDTO>(b)).ToList());
		}

		[HttpPost("/Add")]
		public async Task<ActionResult> PostBill([FromBody] BillDTO dto)
		{
			if (!await context.Patients.AnyAsync(p => p.Id == dto.PatientId)) return NotFound("Doctor Not Found");
			if (!await context.Appointments.AnyAsync(d => d.Id == dto.AppointmentId)) return NotFound("Appointment Not Found");
			if (!dto.Prescriptions.Any()) return BadRequest("No Prescriptions Selected");
			if (!BillIsValid(dto)) return BadRequest("Invalid Bill Data");

			var prescriptions = await context.Prescriptions.Where(p => dto.Prescriptions.Contains(p.Id)).ToListAsync();
			if (!prescriptions.Any()) return NotFound("No Prescriptions Found");

			Bill bill = mapper.Map<BillDTO, Bill>(dto);

			bill.Prescriptions = prescriptions;

			foreach (Prescription p in prescriptions)
			{
				p.BillId = bill.Id;
			}

			await context.Bills.AddAsync(bill);
			await context.SaveChangesAsync();
			return Created();
		}

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchBill([FromRoute] Guid id, [FromBody] BillDTO dto)
		{
			if (!BillIsValid(dto)) return BadRequest("Invalid Bill Data");
			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
			if (bill is null) return NotFound("Bill Not Found");

			if (dto.PaymentMethod != string.Empty) bill.PaymentMethod = dto.PaymentMethod;
			if (dto.Date != null) bill.Date = dto.Date;
			if (dto.PatientId != Guid.Empty) bill.PatientId = dto.PatientId;
			if (dto.AppointmentId != Guid.Empty) bill.AppointmentId = dto.AppointmentId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("/{id:Guid}")]
		public async Task<ActionResult> DeleteBill([FromRoute] Guid id)
		{
			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
			if (bill is null) return NotFound("Bill Not Found");

			context.Bills.Remove(bill);
			await context.SaveChangesAsync();
			return Ok();
		}

		public bool BillIsValid(BillDTO bill)
		{
			if (bill.Amount <= 0) return false;
			if (string.IsNullOrEmpty(bill.PaymentMethod)) return false;
			if (bill.Date is null || bill.Date <= new DateTime(2025,1,1)) return false;
			if (bill.PatientId == Guid.Empty) return false;
			if (bill.AppointmentId == Guid.Empty) return false;
			if (!bill.Prescriptions.Any()) return false;
			return true;
		}
	}
}
