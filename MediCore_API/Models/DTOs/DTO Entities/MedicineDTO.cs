namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class MedicineDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public float Price { get; set; } = 0;
		public string Manufacturer { get; set; } = string.Empty;
	}
}
