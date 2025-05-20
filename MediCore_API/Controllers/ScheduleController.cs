using MediCore_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly MediCoreContext context;
        private readonly IModelMapper mapper;
		private readonly ITimeSlotHandler timeSlotHandler;

        public ScheduleController(MediCoreContext context, IModelMapper mapper, ITimeSlotHandler timeSlotHandler)
        {
            this.context = context;
            this.mapper = mapper;
			this.timeSlotHandler = timeSlotHandler;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleDTO>>> GetAllSchedules()
        {
			try
			{
				var schedules = await context.Schedules.ToListAsync();
				return Ok(schedules.Select(s => mapper.Map<Schedule, ScheduleDTO>(s)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpGet("doctor/{id:Guid}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetDoctorSchedules([FromRoute] Guid id)
        {
			try
			{
				var schedules = await context.Schedules.Where(s => s.DoctorId == id).ToListAsync();
				return Ok(schedules.Select(s => mapper.Map<Schedule, ScheduleDTO>(s)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule([FromRoute] Guid id)
        {
			try
			{
				var schedule = await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
				if (schedule is null) return NotFound("Schedule Not Found");
				return Ok(mapper.Map<Schedule, ScheduleDTO>(schedule));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpPost]
        public async Task<ActionResult> PostSchedule([FromBody] ScheduleDTO dto)
        {
			try
			{
				if (dto.Start > dto.End) return BadRequest("Invlading Schedule Times");
				Schedule schedule = mapper.Map<ScheduleDTO, Schedule>(dto);
				await context.Schedules.AddAsync(schedule);
				await context.SaveChangesAsync();

				List<TimeSlot> timeSlots = timeSlotHandler.CreateTimeSlots(schedule);
				await context.TimeSlots.AddRangeAsync(timeSlots);
				await context.SaveChangesAsync();

				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

        [HttpDelete("{id:Guid}")]
        public async  Task<ActionResult> DeleteSchedule([FromRoute] Guid id)
        {
			try
			{
				var schedule = await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
				if (schedule is null) return NotFound("Schedule Not Found");
				var timeSlots = await context.TimeSlots.Where(t => t.ScheduleId == id).ToListAsync();

				context.Schedules.Remove(schedule);
				context.TimeSlots.RemoveRange(timeSlots);
				await context.SaveChangesAsync();
				return Ok("Schedule Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
    }
}
