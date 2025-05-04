using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Services;
using MediCore_API.Models.DTOs;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public PrescriptionController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetAllPrescriptions()
		{
			try
			{
				var prescriptions = await context.Prescriptions.ToListAsync();
				if (!prescriptions.Any()) return NotFound("No Prescriptions Found");
				return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetPatientPrescriptions([FromRoute] Guid id)
		{
			try
			{
				var prescriptions = await context.Prescriptions.Where(p => p.PatientId == id).ToListAsync();
				if (!prescriptions.Any()) return NotFound("Prescriptions Not Found");
				return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Doctor/{id:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetDoctorPrescriptions([FromRoute] Guid id)
		{
			try
			{
				var prescriptions = await context.Prescriptions.Where(p => p.DoctorId == id).ToListAsync();
				if (!prescriptions.Any()) return NotFound("Prescriptions Not Found");
				return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Doctor/{doctorId:Guid}/Patient/{patientId:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetDoctorPrescriptions([FromRoute] Guid doctorId, [FromRoute] Guid patientId)
		{
			try
			{
				var prescriptions = await context.Prescriptions.Where(p => p.DoctorId == doctorId && p.PatientId == patientId).ToListAsync();
				if (!prescriptions.Any()) return NotFound("Prescriptions Not Found");
				return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("/Create")]
		public async Task<ActionResult> PostPrescription([FromBody] PrescriptionDTO dto)
		{
			try
			{
				if (dto is null || !PrescriptionIsValid(dto)) return BadRequest("Invalid Prescription Data");
				await context.Prescriptions.AddAsync(mapper.Map<PrescriptionDTO, Prescription>(dto));
				await context.SaveChangesAsync();
				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchPrescription([FromRoute] Guid id, [FromBody] PrescriptionDTO dto)
		{
			try
			{
				if (dto is null) return BadRequest("Invalid Prescription Data");
				var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
				if (prescription is null) return NotFound("Prescription Not Found");

				if (dto.Quantity > 0) prescription.Quantity = dto.Quantity;
				if (dto.MedicineId != Guid.Empty) prescription.MedicineId = dto.MedicineId;
				if (dto.DoctorId != Guid.Empty) prescription.DoctorId = dto.DoctorId;
				if (dto.PatientId != Guid.Empty) prescription.PatientId = dto.PatientId;
				if (dto.BillId != Guid.Empty) prescription.BillId = dto.BillId;

				await context.SaveChangesAsync();
				return Ok("Prescription Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("/Delete/{id:Guid}")]
		public async Task<ActionResult> DeletePrescription([FromRoute] Guid id)
		{
			try
			{
				var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
				if (prescription is null) return NotFound("Prescription Not Found");
				context.Prescriptions.Remove(prescription);
				await context.SaveChangesAsync();
				return Ok("Prescription Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		public bool PrescriptionIsValid(PrescriptionDTO prescription)
		{
			if (prescription.Quantity <= 0) return false;
			if (prescription.MedicineId == Guid.Empty) return false;
			if (prescription.DoctorId == Guid.Empty) return false;
			if (prescription.PatientId == Guid.Empty) return false;
			return true;
		}
	}
}
