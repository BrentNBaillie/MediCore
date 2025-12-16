using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly MediCoreContext context;
        private readonly IMapper mapper;
		private readonly ITimeSlotHandler timeSlotHandler;

        public ScheduleController(MediCoreContext context, IMapper mapper, ITimeSlotHandler timeSlotHandler)
        {
            this.context = context;
            this.mapper = mapper;
			this.timeSlotHandler = timeSlotHandler;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleDTO>>> GetAllSchedules()
        {
			var schedules = await context.Schedules
				.ProjectTo<ScheduleDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(schedules);
		}

        [HttpGet("doctor/{id:Guid}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetDoctorSchedules([FromRoute] Guid id)
        {
			var schedules = await context.Schedules
				.Where(s => s.DoctorId == id)
				.ProjectTo<ScheduleDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(schedules);
		}

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule([FromRoute] Guid id)
        {
			var schedule = await context.Schedules
				.ProjectTo<ScheduleDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(s => s.Id == id);
			if (schedule is null) return NotFound("Schedule Not Found");
			return Ok(schedule);
		}

        [HttpPost]
        public async Task<ActionResult> PostSchedule([FromBody] ScheduleDTO dto)
        {
			if (dto.Start > dto.End) return BadRequest("Invlading Schedule Times");
			Schedule schedule = mapper.Map<Schedule>(dto);
			await context.Schedules.AddAsync(schedule);
			await context.SaveChangesAsync();

			List<TimeSlot> timeSlots = timeSlotHandler.CreateTimeSlots(schedule);
			await context.TimeSlots.AddRangeAsync(timeSlots);
			await context.SaveChangesAsync();

			return Created();
		}

        [HttpDelete("{id:Guid}")]
        public async  Task<ActionResult> DeleteSchedule([FromRoute] Guid id)
        {
			var schedule = await context.Schedules.FindAsync(id);
			if (schedule is null) return NotFound("Schedule Not Found");
			var timeSlots = await context.TimeSlots.Where(t => t.ScheduleId == id).ToListAsync();

			context.Schedules.Remove(schedule);
			context.TimeSlots.RemoveRange(timeSlots);
			await context.SaveChangesAsync();
			return Ok("Schedule Deleted");
		}
    }
}
