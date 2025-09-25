using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public PrescriptionController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetAllPrescriptions()
		{
			var prescriptions = await context.Prescriptions.ToListAsync();
			return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<PrescriptionDTO>> GetPrescription([FromRoute] Guid id)
		{
			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
			if (prescription is null) return NotFound("Prescription Not Found");
			return Ok(mapper.Map<Prescription, PrescriptionDTO>(prescription));
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetPatientPrescriptions([FromRoute] Guid id)
		{
			var prescriptions = await context.Prescriptions.Where(p => p.PatientId == id).ToListAsync();
			return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
		}

		[HttpGet("doctor/{id:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetDoctorPrescriptions([FromRoute] Guid id)
		{
			var prescriptions = await context.Prescriptions.Where(p => p.DoctorId == id).ToListAsync();
			if (!prescriptions.Any()) return NotFound("Prescriptions Not Found");
			return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
		}

		[HttpGet("doctor/{doctorId:Guid}/patient/{patientId:Guid}")]
		public async Task<ActionResult<List<PrescriptionDTO>>> GetDoctorPrescriptions([FromRoute] Guid doctorId, [FromRoute] Guid patientId)
		{
			var prescriptions = await context.Prescriptions.Where(p => p.DoctorId == doctorId && p.PatientId == patientId).ToListAsync();
			if (!prescriptions.Any()) return NotFound("Prescriptions Not Found");
			return Ok(prescriptions.Select(p => mapper.Map<Prescription, PrescriptionDTO>(p)).ToList());
		}

		[HttpPost]
		public async Task<ActionResult> PostPrescription([FromBody] PrescriptionDTO dto)
		{
			if (dto is null || !validate.PrescriptionIsValid(dto)) return BadRequest("Invalid Prescription Data");
			await context.Prescriptions.AddAsync(mapper.Map<PrescriptionDTO, Prescription>(dto));
			await context.SaveChangesAsync();
			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchPrescription([FromBody] PrescriptionDTO dto)
		{
			if (dto is null) return BadRequest("Invalid Prescription Data");
			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == dto.Id);
			if (prescription is null) return NotFound("Prescription Not Found");

			if (dto.Quantity > 0) prescription.Quantity = dto.Quantity;
			if (dto.MedicineId != Guid.Empty) prescription.MedicineId = dto.MedicineId;
			if (dto.DoctorId != Guid.Empty) prescription.DoctorId = dto.DoctorId;
			if (dto.PatientId != Guid.Empty) prescription.PatientId = dto.PatientId;
			if (dto.BillId != Guid.Empty) prescription.BillId = dto.BillId;

			await context.SaveChangesAsync();
			return Ok("Prescription Updated");
		}

		[HttpPatch("{prescriptionId:Guid}/set-bill/{billId:Guid}")]
		public async Task<ActionResult> SetPrescriptionBill([FromRoute] Guid prescriptionId, [FromRoute] Guid billId)
		{
			var prescription = await context.Prescriptions.Include(p => p.Medicine).FirstOrDefaultAsync(p => p.Id == prescriptionId);
			if (prescription is null) return NotFound("Prescription Not Found");
			prescription.BillId = billId;

			var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == billId);
			if (bill is null) return NotFound("Bill Not Found");
			var prescriptions = await context.Prescriptions.Include(p => p.Medicine).ToListAsync();
			bill.Amount = prescriptions.Sum(p => p.Quantity * p.Medicine!.Price);

			await context.SaveChangesAsync();
			return Ok("Prescription Bill Id Set");
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeletePrescription([FromRoute] Guid id)
		{
			var prescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
			if (prescription is null) return NotFound("Prescription Not Found");
			context.Prescriptions.Remove(prescription);
			await context.SaveChangesAsync();
			return Ok("Prescription Deleted");
		}
	}
}
