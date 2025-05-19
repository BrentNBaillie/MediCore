using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Address
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string ProvinceOrState { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
	}
}
