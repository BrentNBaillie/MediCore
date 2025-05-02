using MediCore_API.Models.DTOs;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using MediCore_API.Interfaces;
using MediCore_API.Services;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Models.Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalRecordController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public MedicalRecordController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<MedicalRecordDTO>>> GetAllMedicalRecords()
		{
			var records = await context.MedicalRecords.ToListAsync();
			if (!records.Any()) return NotFound("No Medical Records Found");

			return Ok(records.Select(r => mapper.Map<MedicalRecord, MedicalRecordDTO>(r)).ToList());
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<MedicalRecordDTO>> GetMedicalRecord([FromRoute] Guid id)
		{
			var record = await context.MedicalRecords.FirstOrDefaultAsync(r => r.Id == id);
			if (record is null) return NotFound("Medical Record Not Found");

			return Ok(mapper.Map<MedicalRecord, MedicalRecordDTO>(record));
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<MedicalRecordDTO>> GetPatientMedicalRecord([FromRoute] Guid id)
		{
			var record = await context.MedicalRecords.FirstOrDefaultAsync(r => r.PatientId == id);
			if (record is null) return NotFound("Medical Record Not Found");

			return Ok(mapper.Map<MedicalRecord, MedicalRecordDTO>(record));
		}
	}
}
