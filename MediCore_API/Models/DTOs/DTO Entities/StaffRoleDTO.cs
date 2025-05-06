namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class StaffRoleDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}
