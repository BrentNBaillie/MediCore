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
			try
			{
				var timeSlots = await context.TimeSlots.ToListAsync();
				return Ok(timeSlots.Select(t => mapper.Map<TimeSlot, TimeSlotDTO>(t)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("schedule/{id:Guid}")]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetTimeSlotsBySchedule([FromRoute] Guid id)
		{
			try
			{
				var timeSlots = await context.TimeSlots.Where(t => t.ScheduleId == id).ToListAsync();
				return Ok(timeSlots.Select(t => mapper.Map<TimeSlot, TimeSlotDTO>(t)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<TimeSlotDTO>> GetTimeSlot([FromRoute] Guid id)
		{
			try
			{
				var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
				if (timeSlot is null) return NotFound("Time Slot Not Found");
				return Ok(mapper.Map<TimeSlot, TimeSlotDTO>(timeSlot));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("doctor/{id:Guid}")]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetTimeSlotByDoctor([FromRoute] Guid id)
		{
			try
			{
				var timeSlots = await context.TimeSlots.Include(t => t.Schedule).Where(t => t.Schedule!.DoctorId == id).ToListAsync();
				return Ok(timeSlots.Select(t => mapper.Map<TimeSlot, TimeSlotDTO>(t)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("update/{id:Guid}/is-available/{isAvailable:bool}")]
		public async Task<ActionResult> PatchTimeSlot([FromRoute] Guid id, [FromRoute] bool isAvailable)
		{
			try
			{
				var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(t => t.Id == id);
				if (timeSlot is null) return NotFound("Time Slot Not Found");
				timeSlot.IsAvailable = isAvailable;
				await context.SaveChangesAsync();
				return Ok("Time Slot Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
    }
}