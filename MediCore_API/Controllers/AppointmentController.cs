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
			var appointments = await context.Appointments.ToListAsync();
			if (!appointments.Any()) return NotFound("No Appointments Found");

			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
		{
			var appointment = await context.Appointments.FindAsync(id);
			if (appointment is null) return NotFound("Appointment Not Found");
			return Ok(appointment);
		}

		[HttpGet("/Doctor/{id:Guid}")]
		public async Task<ActionResult<List<Appointment>>> GetDoctorAppointments(Guid id)
		{
			if (!await context.Doctors.AnyAsync(d => d.Id == id)) return NotFound("Doctor Not Found");

			var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor)
														 .Include(a => a.Patient).Where(a => a.DoctorId == id)
														 .ToListAsync();
			if (!appointments.Any()) return NotFound("No Doctor Appointments Found");

			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<Appointment>>> GetPatientAppointments(Guid id)
		{
			if (!await context.Patients.AnyAsync(p => p.Id == id)) return NotFound("Patient Not Found");

			var appointments = await context.Appointments.Include(a => a.TimeSlot).Include(a => a.Doctor)
														 .Include(a => a.Patient).Where(a => a.PatientId == id)
														 .ToListAsync();
			if (!appointments.Any()) return NotFound("No Patient Appointments Found");

			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpPost("/Add")]
		public async Task<ActionResult> PostAppointment([FromBody] AppointmentDTO newAppointment)
		{
			if (newAppointment is null) return BadRequest("Appointment Data Null.");
			if (!AppointmentIsValid(newAppointment)) return BadRequest("Appointment Data Is Invalid.");
			if (await context.Appointments.AnyAsync(a => a.Id == newAppointment.Id)) return BadRequest("Appointment Already Exists.");

			await context.Appointments.AddAsync(mapper.Map<AppointmentDTO, Appointment>(newAppointment));
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchAppointment(Guid id, [FromBody] AppointmentDTO dto)
		{
			if(dto is null) return BadRequest("Appointment Data Null.");
			if (!AppointmentIsValid(dto)) return BadRequest("Appointment Data Is Invalid.");

			var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
			if (appointment is null) return NotFound("Appointment Not Found");

			if(dto.Status != string.Empty) appointment.Status = dto.Status;
			if(dto.TimeSlotId != Guid.Empty) appointment.TimeSlotId = dto.TimeSlotId;
			if(dto.PatientId != Guid.Empty) appointment.PatientId = dto.PatientId;
			if(dto.DoctorId != Guid.Empty) appointment.DoctorId = dto.DoctorId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("/{id:Guid}")]
		public async Task<ActionResult> DeleteAppointment(Guid id)
		{
			var appointment = await context.Appointments.FindAsync(id);
			if (appointment is null) return NotFound("Appointment Not Found");

			context.Appointments.Remove(appointment);
			await context.SaveChangesAsync();

			return Ok();
		}

		public bool AppointmentIsValid(AppointmentDTO newAppointment)
		{
			if (newAppointment.Status == string.Empty 
				|| newAppointment.TimeSlotId == Guid.Empty 
				|| newAppointment.DoctorId == Guid.Empty 
				|| newAppointment.PatientId == Guid.Empty)
			{
				return false;
			}
			return true;
		}
    }
}