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

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<Bill>>> GetPatientBills(Guid id)
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
		public async Task<ActionResult> PostBill(BillDTO newBill)
		{
			newBill.Id = Guid.NewGuid();
			if (!await context.Patients.AnyAsync(p => p.Id == newBill.PatientId)) return NotFound("Doctor Not Found");
			if (!await context.Appointments.AnyAsync(d => d.Id == newBill.AppointmentId)) return NotFound("Appointment Not Found");
			if (!newBill.Prescriptions.Any()) return BadRequest("No Prescriptions Selected");

			var prescriptions = await context.Prescriptions.Where(p => newBill.Prescriptions.Contains(p.Id)).ToListAsync();
			if (!prescriptions.Any()) return NotFound("No Prescriptions Found");

			Bill bill = mapper.Map<BillDTO, Bill>(newBill);

			bill.Prescriptions = prescriptions;

			foreach (Prescription p in prescriptions)
			{
				p.BillId = newBill.Id;
			}

			await context.Bills.AddAsync(bill);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchBill(Guid id, [FromBody] BillDTO dto)
		{

		}
	}
}
