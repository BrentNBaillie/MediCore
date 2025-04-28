using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MediCore_API.Models.Entities
{
	public class Staff
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		public Guid? StaffRoleId { get; set; } = Guid.Empty;
		public StaffRole? StaffRole { get; set; } = null;

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; } = string.Empty;
		public IdentityUser? User { get; set; } = null;
	}
}
