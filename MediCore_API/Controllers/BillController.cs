using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;
		private readonly IModelValidation validate;

		public BillController(MediCoreContext context, IMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<BillDTO>>> GetAllBills()
		{
			var bills = await context.Bills
				.ProjectTo<BillDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(bills);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<BillDTO>> GetBill([FromRoute] Guid id)
		{
			var bill = await context.Bills
				.ProjectTo<BillDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(b => b.Id == id);
			if (bill is null) return NotFound("Bill Not Found");
			return Ok(bill);
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<List<BillDTO>>> GetPatientBills([FromRoute] Guid id)
		{
			if (!await context.Patients.AnyAsync(p => p.Id == id)) return NotFound("Patient Not Found");
			var bills = await context.Bills
				.Include(b => b.Appointment)
				.Include(b => b.Prescriptions)
				.Where(b => b.Appointment!.PatientId == id)
				.ProjectTo<BillDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			if (!bills.Any()) return NotFound("No Bills Found");

			foreach (BillDTO bill in bills)
			{
				bill.Amount = bill.Prescriptions!.Sum(p => p.Quantity * p.Medicine!.Price);
			}

            return Ok(bills);
		}

		[HttpPost]
		public async Task<ActionResult> PostBill([FromBody] BillDTO dto)
		{
			if (!await context.Appointments.AnyAsync(d => d.Id == dto.AppointmentId)) return NotFound("Appointment Not Found");
			if (!dto.Prescriptions!.Any()) return BadRequest("No Prescriptions Selected");
			if (!validate.BillIsValid(dto)) return BadRequest("Invalid Bill Data");

			Bill bill = mapper.Map<Bill>(dto);
			foreach (Prescription p in bill.Prescriptions!)
			{
				p.BillId = bill.Id;
			}

			bill.Amount = bill.Prescriptions.Sum(p => p.Quantity * p.Medicine!.Price);

			await context.Bills.AddAsync(bill);
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchBill([FromBody] BillDTO dto)
		{
			var bill = await context.Bills.FindAsync(dto.Id);
			if (bill is null) return NotFound("Bill Not Found");

			if (!string.IsNullOrEmpty(dto.PaymentMethod)) bill.PaymentMethod = dto.PaymentMethod;
			if (dto.Date is not null) bill.Date = dto.Date;
			if (dto.AppointmentId != Guid.Empty) bill.AppointmentId = dto.AppointmentId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteBill([FromRoute] Guid id)
		{
			var bill = await context.Bills.FindAsync(id);
			if (bill is null) return NotFound("Bill Not Found");

			context.Bills.Remove(bill);
			await context.SaveChangesAsync();
			return Ok("Bill Deleted");
		}
	}
}
