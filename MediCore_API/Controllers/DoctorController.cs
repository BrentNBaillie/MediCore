using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using MediCore_API.Data;
using MediCore_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using MediCore_API.Models.DTOs.DTO_Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly UserManager<IdentityUser> userManager;

		public DoctorController(MediCoreContext context, UserManager<IdentityUser> userManager, IModelMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<DoctorDTO>>> GetAllDoctors()
		{
			try
			{
				var doctors = await context.Doctors.ToListAsync();
				return Ok(doctors.Select(d => mapper.Map<Doctor, DoctorDTO>(d)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<DoctorDTO>> GetDoctor([FromRoute] Guid id)
		{
			try
			{
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
				if (doctor is null) return NotFound("Doctor Not Found");

				return Ok(mapper.Map<Doctor, DoctorDTO>(doctor));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("Update")]
		public async Task<ActionResult> PatchDoctor([FromBody] DoctorDTO dto)
		{
			try
			{
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == dto.Id);
				if (doctor is null) return NotFound("Doctor Not Found");

				if (!string.IsNullOrEmpty(dto.FirstName)) doctor.FirstName = dto.FirstName;
				if (!string.IsNullOrEmpty(dto.LastName)) doctor.LastName = dto.LastName;
				if (!string.IsNullOrEmpty(dto.Specialization)) doctor.Specialization = dto.Specialization;
				if (!string.IsNullOrEmpty(dto.PhoneNumber)) doctor.PhoneNumber = dto.PhoneNumber;
				if (!string.IsNullOrEmpty(dto.HospitalName)) doctor.HospitalName = dto.HospitalName;
				if (!string.IsNullOrEmpty(dto.ProfessionalBio)) doctor.ProfessionalBio = dto.ProfessionalBio;

				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteDoctor(Guid id)
		{
			try
			{
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
				if (doctor is null) return NotFound("Doctor Not Found");
				var user = await userManager.FindByIdAsync(doctor.UserId);
				if (user is null) return NotFound("User Not Found");

				context.Doctors.Remove(doctor);
				await userManager.DeleteAsync(user);

				await context.SaveChangesAsync();
				return Ok("Doctor Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
    }
}
