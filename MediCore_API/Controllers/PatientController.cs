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
			var patients = await context.Patients.ToListAsync();
			if (patients is null) return NotFound("Patients Not Found");
			return Ok(patients.Select(p => mapper.Map<Patient, PatientDTO>(p)).ToList());
		}

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<PatientDTO>> GetPatient([FromRoute] Guid id)
        {
			var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
			if (patient is null) return NotFound("Patient Not Found");
			return Ok(mapper.Map<Patient, PatientDTO>(patient));
		}

        [HttpPatch]
        public async Task<ActionResult> PatchPatient([FromBody] PatientDTO dto)
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

		[HttpPatch("{patientId:Guid}/set-address/{addressId:Guid}")]
		public async Task<ActionResult> SetPatientAddress([FromRoute] Guid patientId, [FromRoute] Guid addressId)
		{
			var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
			if (patient is null) return NotFound("Patient Not Found");
			if ((await context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId)) is null) return NotFound("Address Not Found");
			if (addressId != Guid.Empty) patient.AddressId = addressId;
			await context.SaveChangesAsync();
			return Ok("Patient Address Set");
		}

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeletePatient([FromRoute] Guid id)
        {
			var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
			if (patient is null) return NotFound("Patient Not Found");
			var user = await userManager.FindByIdAsync(patient.UserId.ToString()!);
			if (user is null) return NotFound("User Not Found");

			context.Patients.Remove(patient);
			await userManager.DeleteAsync(user);

			await context.SaveChangesAsync();
			return Ok("Patient Deleted");
		}
    }
}
