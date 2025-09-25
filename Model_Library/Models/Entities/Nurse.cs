using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_Library.Models.Identities;

namespace MediCore_Library.Models.Entities
{
	public class Nurse
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;

		[ForeignKey("ApplicationUser")]
		public Guid? UserId { get; set; }
		public ApplicationUser? User { get; set; }
	}
}
