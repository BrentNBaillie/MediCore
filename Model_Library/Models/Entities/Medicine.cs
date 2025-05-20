using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Entities
{
	public class Medicine
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public float Price { get; set; } = 0;
		public string Manufacturer { get; set; } = string.Empty;
	}
}
