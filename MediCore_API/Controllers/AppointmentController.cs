using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public AppointmentController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<AppointmentDTO>>> GetAllAppointments()
		{
			var appointments = await context.Appointments.ToListAsync();
			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<AppointmentDTO>> GetAppointment([FromRoute] Guid id)
		{
			var appointment = await context.Appointments.FindAsync(id);
			if (appointment is null) return NotFound("Appointment Not Found");
			return Ok(mapper.Map<Appointment, AppointmentDTO>(appointment));
		}

		[HttpGet("doctor/{id:Guid}")]
		public async Task<ActionResult<List<AppointmentDTO>>> GetDoctorAppointments([FromRoute] Guid id)
		{
			if (!await context.Doctors.AnyAsync(d => d.Id == id)) return NotFound("Doctor Not Found");

			var appointments = await context.Appointments.Include(a => a.TimeSlot).ThenInclude(t => t!.Schedule).ThenInclude(s => s!.Doctor)
														 .Where(a => a.TimeSlot!.Schedule!.DoctorId == id)
														 .Include(a => a.Patient).ToListAsync();
			if (!appointments.Any()) return NotFound("No Doctor Appointments Found");

			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<List<AppointmentDTO>>> GetPatientAppointments([FromRoute] Guid id)
		{
			if (!await context.Patients.AnyAsync(p => p.Id == id)) return NotFound("Patient Not Found");

			var appointments = await context.Appointments.Include(a => a.TimeSlot)
														 .Include(a => a.Patient).Where(a => a.PatientId == id)
														 .ToListAsync();
			if (!appointments.Any()) return NotFound("No Patient Appointments Found");

			return Ok(appointments.Select(a => mapper.Map<Appointment, AppointmentDTO>(a)).ToList());
		}

		[HttpPost]
		public async Task<ActionResult> PostAppointment([FromBody] AppointmentDTO dto)
		{
			if (dto is null) return BadRequest("Appointment Data Null.");
			if (!validate.AppointmentIsValid(dto)) return BadRequest("Appointment Data Is Invalid.");

			await context.Appointments.AddAsync(mapper.Map<AppointmentDTO, Appointment>(dto));
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchAppointment([FromBody] AppointmentDTO dto)
		{
			if (dto is null) return BadRequest("Appointment Data Null.");

			var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == dto.Id);
			if (appointment is null) return NotFound("Appointment Not Found");

			if (dto.Status != string.Empty) appointment.Status = dto.Status;
			if (dto.TimeSlotId != Guid.Empty) appointment.TimeSlotId = dto.TimeSlotId;
			if (dto.PatientId != Guid.Empty) appointment.PatientId = dto.PatientId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteAppointment([FromRoute] Guid id)
		{
			var appointment = await context.Appointments.FindAsync(id);
			if (appointment is null) return NotFound("Appointment Not Found");

			context.Appointments.Remove(appointment);
			await context.SaveChangesAsync();

			return Ok("Appointment Deleted");
		}
    }
}