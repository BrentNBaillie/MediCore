namespace MediCore_API.Models.DTOs
{
	public class AddressDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string ProvinceOrState { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
	}
}
