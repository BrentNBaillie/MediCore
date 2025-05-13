using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MediCore_API.Models.Entities
{
	public class Staff
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		public Guid? RoleId { get; set; }
		public StaffRole? Role { get; set; }

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; }
	}
}
