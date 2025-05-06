namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class PatientDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateOnly? DateOfBirth { get; set; } = null;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid AddressId { get; set; } = Guid.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
