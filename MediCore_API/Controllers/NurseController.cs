using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NurseController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;
		private readonly UserManager<ApplicationUser> userManager;

		public NurseController(MediCoreContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<List<NurseDTO>>> GetAllStaff()
		{
			var nurse = await context.Nurses
				.ProjectTo<NurseDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(nurse);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<NurseDTO>> GetStaffMember([FromRoute] Guid id)
		{
			var nurse = await context.Nurses
				.ProjectTo<NurseDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(s => s.Id == id);
			if (nurse is null) return NotFound("Staff Member Not Found");
			return Ok(nurse);
		}

		[HttpPatch]
		public async Task<ActionResult> PatchStaffMember([FromBody] NurseDTO dto)
		{
			if (dto is null) return BadRequest("Invalid Staff Data");
			var nurse = await context.Nurses.FirstOrDefaultAsync(s => s.Id == dto.Id);
			if (nurse is null) return NotFound("Staff Member Not Found");

			if (!string.IsNullOrEmpty(dto.FirstName)) nurse.FirstName = dto.FirstName;
			if (!string.IsNullOrEmpty(dto.PhoneNumber)) nurse.PhoneNumber = dto.PhoneNumber;

			await context.SaveChangesAsync();
			return Ok("Staff Member Updated");
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteStaffMember([FromRoute] Guid id)
		{
			var nurse = await context.Nurses.FirstOrDefaultAsync(s => s.Id == id);
			if (nurse is null) return NotFound("Staff Member Not Found");
			var user = await userManager.FindByIdAsync(nurse.UserId.ToString()!);
			if (user is null) return NotFound("User Not Found");

			context.Nurses.Remove(nurse);
			await userManager.DeleteAsync(user);

			await context.SaveChangesAsync();
			return Ok("Staff Member Deleted");
		}
	}
}
