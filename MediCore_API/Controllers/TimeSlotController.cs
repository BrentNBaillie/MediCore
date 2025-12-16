using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TimeSlotController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;

		public TimeSlotController(MediCoreContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetAllTimeSlots()
		{
			var timeSlots = await context.TimeSlots
				.ProjectTo<TimeSlotDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok();
		}

		[HttpGet("schedule/{id:Guid}")]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetTimeSlotsBySchedule([FromRoute] Guid id)
		{
			var timeSlots = await context.TimeSlots
				.Where(t => t.ScheduleId == id)
				.ProjectTo<TimeSlotDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(timeSlots);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<TimeSlotDTO>> GetTimeSlot([FromRoute] Guid id)
		{
			var timeSlot = await context.TimeSlots
				.ProjectTo<TimeSlotDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(t => t.Id == id);
			if (timeSlot is null) return NotFound("Time Slot Not Found");
			return Ok(timeSlot);
		}

		[HttpGet("doctor/{id:Guid}")]
		public async Task<ActionResult<List<TimeSlotDTO>>> GetTimeSlotByDoctor([FromRoute] Guid id)
		{
			var timeSlots = await context.TimeSlots
				.Include(t => t.Schedule)
				.Where(t => t.Schedule!.DoctorId == id)
				.ProjectTo<TimeSlotDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(timeSlots);
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