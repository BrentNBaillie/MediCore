using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_Library.Models.DTOs.DTO_Entities;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController(IAddressRepo addressRepo) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<List<AddressDTO>>> GetAllAddresses()
		{
			var result = await addressRepo.GetAllAddresses();
			return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error.Description);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<AddressDTO>> GetAddress([FromRoute] Guid id)
		{
			var result = await addressRepo.GetAddress(id);
			return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error.Description); ;
		}

		[HttpPost]
		public async Task<ActionResult> PostAddress([FromBody] AddressDTO dto)
		{
			var result = await addressRepo.PostAddress(dto);
			return result.IsSuccess ? Created() : BadRequest(result.Error.Description);
		}

		[HttpPatch]
		public async Task<ActionResult> PatchAddress([FromBody] AddressDTO dto)
		{
			var result = await addressRepo.PatchAddress(dto);
			return result.IsSuccess ? Ok("Address Updated") : BadRequest(result.Error.Description);
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteAddress([FromRoute] Guid id)
		{
			var result = await addressRepo.DeleteAddress(id);
			return result.IsSuccess ? Ok("Address Deleted") : BadRequest(result.Error.Description);
		}
	}
}
