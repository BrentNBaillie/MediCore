using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Interfaces;
using MediCore_API.Services;
using MediCore_API.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly UserManager<IdentityUser> userManager;

		public StaffController(MediCoreContext context, UserManager<IdentityUser> userManager)
		{
			this.context = context;
			mapper = new ModelMapper();
			this.userManager = userManager;
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<StaffDTO>>> GetAllStaff()
		{
			try
			{
				var staff = await context.StaffMembers.ToListAsync();
				return Ok(staff.Select(s => mapper.Map<Staff, StaffDTO>(s)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("/{id:Guid}")]
		public async Task<ActionResult<StaffDTO>> GetStaffMember([FromRoute] Guid id)
		{
			try
			{
				var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == id);
				if (staff is null) return NotFound("Staff Member Not Found");
				return Ok(mapper.Map<Staff, StaffDTO>(staff));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("/Update")]
		public async Task<ActionResult> PatchStaffMember([FromBody] StaffDTO dto)
		{
			try
			{
				if (dto is null) return BadRequest("Invalid Staff Data");
				var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.Id == dto.Id);
				if (staff is null) return NotFound("Staff Member Not Found");

				if (!string.IsNullOrEmpty(dto.Name)) staff.Name = dto.Name;
				if (!string.IsNullOrEmpty(dto.PhoneNumber)) staff.PhoneNumber = dto.PhoneNumber;
				if (dto.RoleId != Guid.Empty) staff.StaffRoleId = dto.RoleId;

				await context.SaveChangesAsync();
				return Ok("Staff Member Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("/{id:Guid}/Delete")]
		public async Task<ActionResult> DeleteStaffMember([FromRoute] Guid id)
		{
			try
			{
				var staff = context.StaffMembers.FirstOrDefault(s => s.Id == id);
				if (staff is null) return NotFound("Staff Member Not Found");
				var user = await userManager.FindByIdAsync(staff.UserId);
				if (user is null) return NotFound("User Not Found");

				context.StaffMembers.Remove(staff);
				await userManager.DeleteAsync(user);

				await context.SaveChangesAsync();
				return Ok("Staff Member Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
