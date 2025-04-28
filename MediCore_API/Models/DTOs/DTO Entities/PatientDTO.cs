namespace MediCore_API.Models.DTOs
{
	public class PatientDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateOnly? DateOfBirth { get; set; } = null;
		public string PhoneNumber { get; set; } = string.Empty;
		public Guid AddressId { get; set; } = Guid.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
