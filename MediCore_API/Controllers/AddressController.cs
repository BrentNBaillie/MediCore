using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public AddressController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<AddressDTO>>> GetAllAddresses()
		{
			var addresses = await context.Addresses.ToListAsync();
			return Ok(addresses.Select(a => mapper.Map<Address, AddressDTO>(a)).ToList());
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<AddressDTO>> GetAddress([FromRoute] Guid id)
		{
			var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
			if (address is null) return NotFound("Address Not Found");
			return Ok(mapper.Map<Address, AddressDTO>(address));
		}

		[HttpPost]
		public async Task<ActionResult> PostAddress([FromBody] AddressDTO dto)
		{
			if (!validate.AddressIsValid(dto)) return BadRequest("Invalid Address Data");
			await context.Addresses.AddAsync(mapper.Map<AddressDTO, Address>(dto));
			await context.SaveChangesAsync();
			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchAddress([FromBody] AddressDTO dto)
		{
			if (dto is null) return BadRequest("Invalid Address Data");
			var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == dto.Id);
			if (address is null) return NotFound("Address Not Found");

			if (!string.IsNullOrEmpty(dto.Street)) address.Street = dto.Street;
			if (!string.IsNullOrEmpty(dto.City)) address.City = dto.City;
			if (!string.IsNullOrEmpty(dto.ProvinceOrState)) address.ProvinceOrState = dto.ProvinceOrState;
			if (!string.IsNullOrEmpty(dto.Country)) address.Country = dto.Country;
			if (!string.IsNullOrEmpty(dto.PostalCode)) address.PostalCode = dto.PostalCode;

			await context.SaveChangesAsync();
			return Ok("Address Updated");
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteAddress([FromRoute] Guid id)
		{
			var address = await context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
			if (address is null) return NotFound("Address Not Found");
			context.Addresses.Remove(address);
			await context.SaveChangesAsync();
			return Ok("Address Deleted");
		}
	}
}
