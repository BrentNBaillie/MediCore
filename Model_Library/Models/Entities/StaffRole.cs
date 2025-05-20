using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Entities
{
	public class StaffRole
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}
