namespace MediCore_Library.Models.DTOs.DTO_Entities.Full
{
	public class PatientFullDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateOnly? DateOfBirth { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
		public AddressDTO? AddressId { get; set; }
		public string UserId { get; set; } = string.Empty;
	}
}
