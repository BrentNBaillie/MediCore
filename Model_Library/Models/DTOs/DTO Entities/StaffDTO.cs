namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class StaffDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid? RoleId { get; set; }
		public Guid? UserId { get; set; }
	}
}
