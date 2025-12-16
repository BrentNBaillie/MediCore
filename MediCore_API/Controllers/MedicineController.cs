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
	public class MedicineController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IMapper mapper;
		private readonly IModelValidation validate;

		public MedicineController(MediCoreContext context, IMapper mapper, IModelValidation validate)
		{
			this.context = context;
			this.mapper = mapper;
			this.validate = validate;
		}

		[HttpGet]
		public async Task<ActionResult<List<MedicineDTO>>> GetAllMedicine()
		{
			var medicines = await context.Medicines
				.ProjectTo<MedicineDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(medicines);
		}

		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<MedicineDTO>> GetMedicine([FromRoute] Guid id)
		{
			var medicine = await context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
			if (medicine is null) return NotFound("Medicine Not Found");
			return Ok(mapper.Map<MedicineDTO>(medicine));
		}

		[HttpPost]
		public async Task<ActionResult> PostMedicine([FromBody] MedicineDTO dto)
		{
			if (!validate.MedicineIsValid(dto)) return BadRequest("Invalid Medicine Data");
			await context.Medicines.AddAsync(mapper.Map<Medicine>(dto));
			await context.SaveChangesAsync();
			return Created();
		}

		[HttpPatch]
		public async Task<ActionResult> PatchMedicine([FromBody] MedicineDTO dto)
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

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteMedicine([FromRoute] Guid id)
		{
			var medicine = await context.Medicines.FindAsync(id);
			if (medicine is null) return NotFound("Medicine Not Found");
			context.Medicines.Remove(medicine);
			await context.SaveChangesAsync();
			return Ok("Medicine Deleted");
		}
	}
}
