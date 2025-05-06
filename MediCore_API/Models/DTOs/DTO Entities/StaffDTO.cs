namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class StaffDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid RoleId { get; set; } = Guid.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
