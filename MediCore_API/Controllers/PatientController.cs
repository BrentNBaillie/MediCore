using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;

namespace MediCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly MediCoreContext context;
        private readonly IModelMapper mapper;
		private readonly UserManager<ApplicationUser> userManager;

		public PatientController(MediCoreContext context, UserManager<ApplicationUser> userManager, IModelMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
			this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetAllPatients()
        {
			try
			{
				var patients = await context.Patients.ToListAsync();
				return Ok(patients.Select(p => mapper.Map<Patient, PatientDTO>(p)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpGet("{id:Guid}")]
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

        [HttpPatch]
        public async Task<ActionResult> PatchPatient([FromBody] PatientDTO dto)
        {
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == dto.Id);
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

		[HttpPatch("{patientId:Guid}/set-address/{addressId:Guid}")]
		public async Task<ActionResult> SetPatientAddress([FromRoute] Guid patientId, [FromRoute] Guid addressId)
		{
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
				if (patient is null) return NotFound("Patient Not Found");
				if ((await context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId)) is null) return NotFound("Address Not Found");
				if (addressId != Guid.Empty) patient.AddressId = addressId;
				await context.SaveChangesAsync();
				return Ok("Patient Address Set");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeletePatient([FromRoute] Guid id)
        {
			try
			{
				var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
				if (patient is null) return NotFound("Patient Not Found");
				var user = await userManager.FindByIdAsync(patient.UserId);
				if (user is null) return NotFound("User Not Found");

				context.Patients.Remove(patient);
				await userManager.DeleteAsync(user);

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
