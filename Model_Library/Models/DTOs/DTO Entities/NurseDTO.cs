namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class NurseDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid? UserId { get; set; }
	}
}
