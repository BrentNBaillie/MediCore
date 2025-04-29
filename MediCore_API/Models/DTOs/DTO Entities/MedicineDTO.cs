namespace MediCore_API.Models.DTOs
{
	public class MedicineDTO
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public float Price { get; set; } = 0;
		public string Manufacturer { get; set; } = string.Empty;
	}
}
