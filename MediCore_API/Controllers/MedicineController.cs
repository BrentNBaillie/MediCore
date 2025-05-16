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
	public class MedicineController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly IModelValidation validate;

		public MedicineController(MediCoreContext context, IModelMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<MedicineDTO>>> GetAllMedicine()
		{
			try
			{
				var medicines = await context.Medicines.ToListAsync();
				return Ok(medicines.Select(m => mapper.Map<Medicine, MedicineDTO>(m)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<MedicineDTO>> GetMedicine([FromRoute] Guid id)
		{
			try
			{
				var medicine = await context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
				if (medicine is null) return NotFound("Medicine Not Found");
				return Ok(mapper.Map<Medicine, MedicineDTO>(medicine));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost]
		public async Task<ActionResult> PostMedicine([FromBody] MedicineDTO dto)
		{
			try
			{
				if (!validate.MedicineIsValid(dto)) return BadRequest("Invalid Medicine Data");
				await context.Medicines.AddAsync(mapper.Map<MedicineDTO, Medicine>(dto));
				await context.SaveChangesAsync();
				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPatch]
		public async Task<ActionResult> PatchMedicine([FromBody] MedicineDTO dto)
		{
			try
			{
				if (dto is null) return BadRequest("Invalid Medicine Data");
				var medicine = await context.Medicines.FirstOrDefaultAsync(m => m.Id == dto.Id);
				if (medicine is null) return NotFound("Medicine Not Found");

				if (!string.IsNullOrEmpty(dto.Name)) medicine.Name = dto.Name;
				if (!string.IsNullOrEmpty(dto.Description)) medicine.Description = dto.Description;
				if (dto.Price > 0) medicine.Price = dto.Price;
				if (!string.IsNullOrEmpty(dto.Manufacturer)) medicine.Manufacturer = dto.Manufacturer;
				await context.SaveChangesAsync();
				return Ok("Medicine Updated");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteMedicine([FromRoute] Guid id)
		{
			try
			{
				var medicine = await context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
				if (medicine is null) return NotFound("Medicine Not Found");
				context.Medicines.Remove(medicine);
				await context.SaveChangesAsync();
				return Ok("Medicine Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
