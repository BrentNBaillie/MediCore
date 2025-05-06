using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Models.Identities;
using MediCore_API.Models.DTOs;
using MediCore_API.Interfaces;
using MediCore_API.Services;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public AppointmentController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<AppointmentDTO>>> GetAllAppointments()
		{
			try
			{
				var appointments = await context.Appointments.ToListAsync();
				return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<AppointmentDTO>> GetAppointment([FromRoute] Guid id)
		{
			try
			{
				var appointment = await context.Appointments.FindAsync(id);
				if (appointment is null) return NotFound("Appointment Not Found");
				return Ok(appointment);
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Doctor/{id:Guid}")]
		public async Task<ActionResult<List<AppointmentDTO>>> GetDoctorAppointments([FromRoute] Guid id)
		{
			try
			{
				if (!await context.Doctors.AnyAsync(d => d.Id == id)) return NotFound("Doctor Not Found");

				var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor)
															 .Include(a => a.Patient).Where(a => a.DoctorId == id)
															 .ToListAsync();
				if (!appointments.Any()) return NotFound("No Doctor Appointments Found");

				return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<AppointmentDTO>>> GetPatientAppointments([FromRoute] Guid id)
		{
			try
			{
				if (!await context.Patients.AnyAsync(p => p.Id == id)) return NotFound("Patient Not Found");

				var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor)
															 .Include(a => a.Patient).Where(a => a.PatientId == id)
															 .ToListAsync();
				if (!appointments.Any()) return NotFound("No Patient Appointments Found");

				return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("/Add")]
		public async Task<ActionResult> PostAppointment([FromBody] AppointmentDTO dto)
		{
			try
			{
				if (dto is null) return BadRequest("Appointment Data Null.");
				if (!AppointmentIsValid(dto)) return BadRequest("Appointment Data Is Invalid.");

				await context.Appointments.AddAsync(mapper.Map<AppointmentDTO, Appointment>(dto));
				await context.SaveChangesAsync();

				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("/Update")]
		public async Task<ActionResult> PatchAppointment([FromBody] AppointmentDTO dto)
		{
			try
			{
				if (dto is null) return BadRequest("Appointment Data Null.");
				if (!AppointmentIsValid(dto)) return BadRequest("Appointment Data Is Invalid.");

				var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == dto.Id);
				if (appointment is null) return NotFound("Appointment Not Found");

				if (dto.Status != string.Empty) appointment.Status = dto.Status;
				if (dto.TimeSlotId != Guid.Empty) appointment.TimeSlotId = dto.TimeSlotId;
				if (dto.PatientId != Guid.Empty) appointment.PatientId = dto.PatientId;
				if (dto.DoctorId != Guid.Empty) appointment.DoctorId = dto.DoctorId;

				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("/{id:Guid}")]
		public async Task<ActionResult> DeleteAppointment([FromRoute] Guid id)
		{
			try
			{
				var appointment = await context.Appointments.FindAsync(id);
				if (appointment is null) return NotFound("Appointment Not Found");

				context.Appointments.Remove(appointment);
				await context.SaveChangesAsync();

				return Ok("Appointment Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		public bool AppointmentIsValid(AppointmentDTO appointment)
		{
			if (string.IsNullOrEmpty(appointment.Status)) return false;
			if (appointment.TimeSlotId == Guid.Empty) return false;
			if (appointment.PatientId == Guid.Empty) return false;
			if (appointment.DoctorId == Guid.Empty) return false;
			return true;
		}
    }
}