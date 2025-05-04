using MediCore_API.Models.DTOs;
using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Services;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly MediCoreContext context;
        private readonly IModelMapper mapper;

        public ScheduleController(MediCoreContext context)
        {
            this.context = context;
            mapper = new ModelMapper();
        }

        [HttpGet("/All")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetAllSchedules()
        {
            var schedules = await context.Schedules.ToListAsync();
            if (!schedules.Any()) return NotFound("No Schedules Found");
            return Ok(schedules.Select(s => mapper.Map<Schedule, ScheduleDTO>(s)).ToList());
        }

        [HttpGet("/Doctor/{id:Guid}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetDoctorSchedules([FromRoute] Guid id)
        {
            var schedules = await context.Schedules.Where(s => s.DoctorId == id).ToListAsync();
            if (!schedules.Any()) return NotFound("Schedules Not Found");
            return Ok(schedules.Select(s => mapper.Map<Schedule, ScheduleDTO>(s)).ToList());
        }

        [HttpGet("/{id:Guid}")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule([FromRoute] Guid id)
        {
            var schedule = await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
            if (schedule is null) return NotFound("Shecdule Not Found");
            return Ok(mapper.Map<Schedule, ScheduleDTO>(schedule));
        }

        [HttpPost("/Create")]
        public async Task<ActionResult> PostSchedule([FromBody] ScheduleDTO dto)
        {
            if (dto.Start < dto.End) return BadRequest("Invlading Schedule Times");
            Schedule schedule = mapper.Map<ScheduleDTO, Schedule>(dto);
            List<TimeSlot> timeSlots = CreateTimeSlots(schedule);

            await context.TimeSlots.AddRangeAsync(timeSlots);
            await context.SaveChangesAsync();

            return Created();
        }

        [HttpDelete("/{id:Guid}")]
        public async  Task<ActionResult> DeleteSchedule([FromRoute] Guid id)
        {
            var schedule = await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);
            if (schedule is null) return NotFound("Schedule Not Found");
            var timeSlots = await context.TimeSlots.Where(t => t.ScheduleId == id).ToListAsync();

             context.Schedules.Remove(schedule);
            context.TimeSlots.RemoveRange(timeSlots);
            await context.SaveChangesAsync(); 
            return Ok("Schedule Deleted");
        }

        public List<TimeSlot> CreateTimeSlots(Schedule schedule)
        {
            TimeOnly start = schedule.Start;
            TimeOnly end = start.AddHours(1);
            int timeSlotCount = (int)(schedule.End.ToTimeSpan() - start.ToTimeSpan()).TotalHours;
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            for (int i = 0; i < timeSlotCount; i++)
            {
                timeSlots.Add(new TimeSlot
                {
                    Start = start,
                    End = end,
                    ScheduleId = schedule.Id
                });
                start = start.AddHours(1);
                end = end.AddHours(1);
            }
            return timeSlots;
        }
    }
}
