namespace MediCore_Library.Models.DTOs.DTO_Entities.Full
{
	public class NurseFullDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
