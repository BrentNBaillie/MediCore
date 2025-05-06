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
	public class StaffRoleController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public StaffRoleController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<StaffRoleDTO>>> GetAllRoles()
		{
			try
			{
				var roles = await context.StaffRoles.ToListAsync();
				return Ok(roles.Select(r => mapper.Map<StaffRole, StaffRoleDTO>(r)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<StaffRoleDTO>> GetStaffRole([FromRoute] Guid id)
		{
			try
			{
				var role = await context.StaffRoles.FirstOrDefaultAsync(r => r.Id == id);
				if (role is null) return NotFound("Staff Role Not Found");
				return Ok(mapper.Map<StaffRole, StaffRoleDTO>(role));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("Create")]
		public async Task<ActionResult> PostStaffRole([FromBody] StaffRoleDTO dto)
		{
			try
			{
				if (!validate.RoleIsValid(dto)) return BadRequest("Invalid Staff Role Data");
				await context.StaffRoles.AddAsync(mapper.Map<StaffRoleDTO, StaffRole>(dto));
				await context.SaveChangesAsync();
				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch("Update")]
		public async Task<ActionResult> PatchStaffRole([FromBody] StaffRoleDTO dto)
		{
			try
			{
				var role = await context.StaffRoles.FirstOrDefaultAsync(r => r.Id == dto.Id);
				if (role is null) return NotFound("Staff Role Not Found");

				if (!string.IsNullOrEmpty(dto.Title)) role.Title = dto.Title;
				if (!string.IsNullOrEmpty(dto.Description)) role.Description = dto.Description;

				await context.SaveChangesAsync();
				return Ok("Staff Role Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteStaffRole([FromRoute] Guid id)
		{
			try
			{
				var role = await context.StaffRoles.FirstOrDefaultAsync(r => r.Id == id);
				if (role is null) return NotFound("Staff Role Not Found");
				context.StaffRoles.Remove(role);
				await context.SaveChangesAsync();
				return Ok("Staff Role Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
