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
	public class FeedbackController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public FeedbackController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<FeedbackDTO>>> GetAllFeedback()
		{
			var feedbacks = await context.Feedbacks.ToListAsync();
			return Ok(feedbacks.Select(f => mapper.Map<Feedback, FeedbackDTO>(f)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<FeedbackDTO>> GetFeedback([FromRoute] Guid id)
		{
			var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
			if (feedback is null) return NotFound("Feedback Not Found");

			return Ok(mapper.Map<Feedback, FeedbackDTO>(feedback));
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<List<FeedbackDTO>>> GetPatientFeedbacks(Guid id)
		{
			var feedbacks = await context.Feedbacks.Where(f => f.PatientId == id).ToListAsync();
			if (!feedbacks.Any()) return NotFound("Feedback Not Found");

			return Ok(feedbacks.Select(f => mapper.Map<Feedback, FeedbackDTO>(f)).ToList());
		}

		[HttpPost]
		public async Task<ActionResult> PostFeedback([FromBody] FeedbackDTO dto)
		{
			if (!validate.FeedbackIsValid(dto)) return BadRequest("Invalid Feedback Data");
			Feedback feedback = mapper.Map<FeedbackDTO, Feedback>(dto);

			await context.Feedbacks.AddAsync(feedback);
			await context.SaveChangesAsync();
			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchFeedback([FromBody] FeedbackDTO dto)
		{
			var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == dto.Id);
			if (feedback is null) return NotFound("Feedback Not Found");

			if (dto.Date is not null) feedback.Date = dto.Date;
			if (!string.IsNullOrEmpty(dto.Details)) feedback.Details = dto.Details;
			if (dto.PatientId != Guid.Empty) feedback.PatientId = dto.PatientId;

			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteFeedback([FromRoute] Guid id)
		{
			var feedback = await context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
			if (feedback is null) return NotFound("Feedback Not Found");

			context.Feedbacks.Remove(feedback);
			await context.SaveChangesAsync();
			return Ok("Feedback Deleted");
		}
	}
}
