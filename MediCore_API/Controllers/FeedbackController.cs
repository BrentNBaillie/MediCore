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
	public class FeedbackController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;
		private readonly IModelValidation validate;

		public FeedbackController(MediCoreContext context, IMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<FeedbackDTO>>> GetAllFeedback()
		{
			var feedbacks = await context.Feedbacks
				.ProjectTo<FeedbackDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(feedbacks);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<FeedbackDTO>> GetFeedback([FromRoute] Guid id)
		{
			var feedback = await context.Feedbacks
				.ProjectTo<FeedbackDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(f => f.Id == id);
			if (feedback is null) return NotFound("Feedback Not Found");

			return Ok(feedback);
		}

		[HttpGet("patient/{id:Guid}")]
		public async Task<ActionResult<List<FeedbackDTO>>> GetPatientFeedbacks(Guid id)
		{
			var feedbacks = await context.Feedbacks
				.Where(f => f.PatientId == id)
				.ProjectTo<FeedbackDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			if (!feedbacks.Any()) return NotFound("Feedback Not Found");

			return Ok(feedbacks);
		}

		[HttpPost]
		public async Task<ActionResult> PostFeedback([FromBody] FeedbackDTO dto)
		{
			if (!validate.FeedbackIsValid(dto)) return BadRequest("Invalid Feedback Data");
			Feedback feedback = mapper.Map<Feedback>(dto);

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
			var feedback = await context.Feedbacks.FindAsync(id);
			if (feedback is null) return NotFound("Feedback Not Found");

			context.Feedbacks.Remove(feedback);
			await context.SaveChangesAsync();
			return Ok("Feedback Deleted");
		}
	}
}
