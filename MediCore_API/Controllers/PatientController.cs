using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly MediCoreContext context;
        private readonly IMapper mapper;
		private readonly UserManager<ApplicationUser> userManager;

		public PatientController(MediCoreContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
			this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetAllPatients()
        {
			var patients = await context.Patients
				.ProjectTo<PatientDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			if (!patients.Any()) return NotFound("Patients Not Found");
			return Ok(patients);
		}

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<PatientDTO>> GetPatient([FromRoute] Guid id)
        {
			var patient = await context.Patients
				.ProjectTo<PatientDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(p => p.Id == id);
			if (patient is null) return NotFound("Patient Not Found");
			return Ok(patient);
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
