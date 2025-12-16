using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalRecordController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;

		public MedicalRecordController(MediCoreContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<MedicalRecordDTO>>> GetAllMedicalRecords()
		{
			var records = await context.MedicalRecords
				.ProjectTo<MedicalRecordDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(records);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<MedicalRecordDTO>> GetMedicalRecord([FromRoute] Guid id)
		{
			var record = await context.MedicalRecords
				.ProjectTo<MedicalRecordDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(r => r.Id == id);
			if (record is null) return NotFound("Medical Record Not Found");

			return Ok(record);
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<MedicalRecordDTO>> GetPatientMedicalRecord([FromRoute] Guid id)
		{
			var record = await context.MedicalRecords
				.ProjectTo<MedicalRecordDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(r => r.PatientId == id);
			if (record is null) return NotFound("Medical Record Not Found");

			return Ok(record);
		}
	}
}
