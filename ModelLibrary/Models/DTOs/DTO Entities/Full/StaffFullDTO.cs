namespace MediCore_API.Models.DTOs.DTO_Entities.Full
{
	public class StaffFullDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public StaffRoleDTO? Role { get; set; }
		public string UserId { get; set; } = string.Empty;
	}
}
