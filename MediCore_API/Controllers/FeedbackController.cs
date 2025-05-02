using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Services;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedbackController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;

		public FeedbackController(MediCoreContext context)
		{
			this.context = context;
			mapper = new ModelMapper();
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<FeedbackDTO>>> GetAllFeedback()
		{
			try
			{
				var feedbacks = await context.Feedbacks.ToListAsync();
				if (!feedbacks.Any()) return NotFound("Feedbacks Not Found");

				return Ok(feedbacks.Select(f => mapper.Map<Feedback, FeedbackDTO>(f)).ToList());
			} 
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<FeedbackDTO>> GetFeedback([FromRoute] Guid id)
		{
			try
			{
				var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
				if (feedback is null) return NotFound("Feedback Not Found");

				return Ok(mapper.Map<Feedback, FeedbackDTO>(feedback));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/Patient/{id:Guid}")]
		public async Task<ActionResult<List<FeedbackDTO>>> GetPatientFeddbacks(Guid id)
		{
			try
			{
				var feedbacks = await context.Feedbacks.Where(f => f.Id == id).ToListAsync();
				if (!feedbacks.Any()) return NotFound("Feedback Not Found");

				return Ok(feedbacks.Select(f => mapper.Map<Feedback, FeedbackDTO>(f)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("/Create")]
		public async Task<ActionResult> PostFeedback([FromBody] FeedbackDTO dto)
		{
			try
			{
				if (!FeedbackIsValid(dto)) return BadRequest("Invalid Feedback Data");
				Feedback feedback = mapper.Map<FeedbackDTO, Feedback>(dto);

				await context.Feedbacks.AddAsync(feedback);
				await context.SaveChangesAsync();
				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("/{id:Guid}/Update")]
		public async Task<ActionResult> PatchFeedback([FromRoute] Guid id, [FromBody] FeedbackDTO dto)
		{
			try
			{
				var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
				if (feedback is null) return NotFound("Feedback Not Found");

				if (dto.Date is not null) feedback.Date = dto.Date;
				if (!string.IsNullOrEmpty(dto.Details)) feedback.Details = dto.Details;
				if (dto.PatientId != Guid.Empty) feedback.PatientId = dto.PatientId;

				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("/{id:Guid}")]
		public async Task<ActionResult> DeleteFeedback([FromRoute] Guid id)
		{
			try
			{
				var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
				if (feedback is null) return NotFound("Feedback Not Found");

				context.Feedbacks.Remove(feedback);
				await context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		public bool FeedbackIsValid(FeedbackDTO feedback)
		{
			if (string.IsNullOrEmpty(feedback.Details)) return false;
			if (feedback.PatientId == Guid.Empty) return false;
			return true;
		}
	}
}
