using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs.DTO_Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public BillController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<BillDTO>>> GetAllBills()
		{
			try
			{
				var bills = await context.Bills.ToListAsync();
				return Ok(bills.Select(b => mapper.Map<Bill, BillDTO>(b)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<BillDTO>> GetBill([FromRoute] Guid id)
		{
			try
			{
				var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
				if (bill is null) return NotFound("Bill Not Found");
				return Ok(mapper.Map<Bill, BillDTO>(bill));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("Patient/{id:Guid}")]
		public async Task<ActionResult<List<BillDTO>>> GetPatientBills([FromRoute] Guid id)
		{
			try
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
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("Create")]
		public async Task<ActionResult> PostBill([FromBody] BillDTO dto)
		{
			try
			{
				if (!await context.Patients.AnyAsync(p => p.Id == dto.PatientId)) return NotFound("Patient Not Found");
				if (!await context.Appointments.AnyAsync(d => d.Id == dto.AppointmentId)) return NotFound("Appointment Not Found");
				if (!dto.Prescriptions.Any()) return BadRequest("No Prescriptions Selected");
				if (!validate.BillIsValid(dto)) return BadRequest("Invalid Bill Data");

				Bill bill = mapper.Map<BillDTO, Bill>(dto);

				await context.Bills.AddAsync(bill);
				await context.SaveChangesAsync();
				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("Update")]
		public async Task<ActionResult> PatchBill([FromBody] BillDTO dto)
		{
			try
			{
				var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == dto.Id);
				if (bill is null) return NotFound("Bill Not Found");

				if (!string.IsNullOrEmpty(dto.PaymentMethod)) bill.PaymentMethod = dto.PaymentMethod;
				if (dto.Date is not null) bill.Date = dto.Date;
				if (dto.PatientId != Guid.Empty) bill.PatientId = dto.PatientId;
				if (dto.AppointmentId != Guid.Empty) bill.AppointmentId = dto.AppointmentId;

				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteBill([FromRoute] Guid id)
		{
			try
			{
				var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == id);
				if (bill is null) return NotFound("Bill Not Found");

				context.Bills.Remove(bill);
				await context.SaveChangesAsync();
				return Ok("Bill Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
