using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using MediCore_API.Data;
using MediCore_API.Models.DTOs;
using MediCore_API.Interfaces;
using MediCore_API.Services;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public DoctorController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<DoctorDTO>>> GetAllDoctors()
		{
			try
			{
				var doctors = await context.Doctors.ToListAsync();
				if (!doctors.Any()) return NotFound("Doctors Not Found");

				return Ok(doctors.Select(d => mapper.Map<Doctor, DoctorDTO>(d)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/{id:Guid}")]
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

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchDoctor([FromRoute] Guid id, [FromBody] DoctorDTO dto)
		{
			try
			{
				if (!await context.Doctors.AnyAsync(d => d.Id == id)) return NotFound("Doctor Not Found");
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
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

		[HttpDelete("/{id:Guid}")]
		public async Task<ActionResult> DeleteDoctor(Guid id)
		{
			try
			{
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
				if (doctor is null) return NotFound("Doctor Not Found");

				context.Doctors.Remove(doctor);
				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		public bool DoctorIsValid(DoctorDTO doctor)
		{
			if (string.IsNullOrEmpty(doctor.FirstName)) return false;
			if (string.IsNullOrEmpty(doctor.LastName)) return false;
			if (string.IsNullOrEmpty(doctor.Specialization)) return false;
			if (string.IsNullOrEmpty(doctor.PhoneNumber)) return false;
			if (string.IsNullOrEmpty(doctor.HospitalName)) return false;
			if (string.IsNullOrEmpty(doctor.ProfessionalBio)) return false;
			return true;
		}
    }
}
