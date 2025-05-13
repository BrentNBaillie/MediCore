using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.Entities
{
	public class Patient
	{
		[Key]
		public Guid? Id { get; set; } = Guid.NewGuid();
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateOnly? DateOfBirth { get; set; } = new DateOnly(2000,1,1);
		public string PhoneNumber { get; set; } = string.Empty;

		public Guid? AddressId { get; set; }
		public Address? Address { get; set; }

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; }

	}
}
