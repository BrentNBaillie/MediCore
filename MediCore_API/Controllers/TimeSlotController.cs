using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs.DTO_Entities;


namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimeSlotController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public TimeSlotController(MediCoreContext context, IModelMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetAllTimeSlots()
		{
			var timeSlots = await context.TimeSlots.ToListAsync();
			return Ok(timeSlots.Select(t => mapper.Map<TimeSlot, TimeSlotDTO>(t)).ToList());
		}

		[HttpGet("schedule/{id:Guid}")]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetTimeSlotsBySchedule([FromRoute] Guid id)
		{
			var timeSlots = await context.TimeSlots.Where(t => t.ScheduleId == id).ToListAsync();
			return Ok(timeSlots.Select(t => mapper.Map<TimeSlot, TimeSlotDTO>(t)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<TimeSlotDTO>> GetTimeSlot([FromRoute] Guid id)
		{
			var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
			if (timeSlot is null) return NotFound("Time Slot Not Found");
			return Ok(mapper.Map<TimeSlot, TimeSlotDTO>(timeSlot));
		}

		[HttpPatch("update/{id:Guid}/is-available/{isAvailable:bool}")]
		public async Task<ActionResult> PatchTimeSlot([FromRoute] Guid id, [FromRoute] bool isAvailable)
		{
			var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
			if (timeSlot is null) return NotFound("Time Slot Not Found");
			timeSlot.IsAvailable = isAvailable;
			await context.SaveChangesAsync();
			return Ok("Time Slot Updated");
		}
    }
}