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
    public class PatientController : ControllerBase
    {
        private readonly MediCoreContext context;
        private readonly IModelMapper mapper;

        public PatientController(MediCoreContext context)
        {
            this.context = context;
            mapper = new ModelMapper();
        }

        [HttpGet("/All")]
        public async Task<ActionResult<List<PatientDTO>>> GetAllPatients()
        {
			try
			{
				var patients = await context.Patients.ToListAsync();
				if (!patients.Any()) return NotFound("No Patients Found");
				return Ok(patients.Select(p => mapper.Map<Patient, PatientDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpGet("/{id:Guid}")]
        public async Task<ActionResult<PatientDTO>> GetPatient([FromRoute] Guid id)
        {
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
				if (patient is null) return NotFound("Patient Not Found");
				return Ok(mapper.Map<Patient, PatientDTO>(patient));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpPatch("/{id:Guid}/Update")]
        public async Task<ActionResult> PatchPatient([FromRoute] Guid id, [FromBody] PatientDTO dto)
        {
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
				if (patient is null) return NotFound("Patient Not Found");

				if (!string.IsNullOrEmpty(dto.FirstName)) patient.FirstName = dto.FirstName;
				if (!string.IsNullOrEmpty(dto.LastName)) patient.LastName = dto.LastName;
				if (!string.IsNullOrEmpty(dto.Gender)) patient.Gender = dto.Gender;
				if (dto.DateOfBirth is not null) patient.DateOfBirth = dto.DateOfBirth;
				if (!string.IsNullOrEmpty(dto.PhoneNumber)) patient.PhoneNumber = dto.PhoneNumber;
				if (dto.AddressId != Guid.Empty) patient.AddressId = dto.AddressId;

				await context.SaveChangesAsync();
				return Ok("Patient Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpDelete("/{id:Guid}/Delete")]
        public async Task<ActionResult> DeletePatient([FromRoute] Guid id)
        {
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
				if (patient is null) return NotFound("Patient Not Found");
				context.Patients.Remove(patient);
				await context.SaveChangesAsync();
				return Ok("Patient Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
    }
}
