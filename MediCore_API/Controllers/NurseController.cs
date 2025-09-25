using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NurseController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly UserManager<ApplicationUser> userManager;

		public NurseController(MediCoreContext context, UserManager<ApplicationUser> userManager, IModelMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<List<NurseDTO>>> GetAllStaff()
		{
			var staff = await context.Nurses.ToListAsync();
			return Ok(staff.Select(s => mapper.Map<Nurse, NurseDTO>(s)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<NurseDTO>> GetStaffMember([FromRoute] Guid id)
		{
			var staff = await context.Nurses.FirstOrDefaultAsync(s => s.Id == id);
			if (staff is null) return NotFound("Staff Member Not Found");
			return Ok(mapper.Map<Nurse, NurseDTO>(staff));
		}

		[HttpPatch]
		public async Task<ActionResult> PatchStaffMember([FromBody] NurseDTO dto)
		{
			if (dto is null) return BadRequest("Invalid Staff Data");
			var staff = await context.Nurses.FirstOrDefaultAsync(s => s.Id == dto.Id);
			if (staff is null) return NotFound("Staff Member Not Found");

			if (!string.IsNullOrEmpty(dto.FirstName)) staff.FirstName = dto.FirstName;
			if (!string.IsNullOrEmpty(dto.PhoneNumber)) staff.PhoneNumber = dto.PhoneNumber;

			await context.SaveChangesAsync();
			return Ok("Staff Member Updated");
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteStaffMember([FromRoute] Guid id)
		{
			var staff = await context.Nurses.FirstOrDefaultAsync(s => s.Id == id);
			if (staff is null) return NotFound("Staff Member Not Found");
			var user = await userManager.FindByIdAsync(staff.UserId.ToString()!);
			if (user is null) return NotFound("User Not Found");

			context.Nurses.Remove(staff);
			await userManager.DeleteAsync(user);

			await context.SaveChangesAsync();
			return Ok("Staff Member Deleted");
		}
	}
}
