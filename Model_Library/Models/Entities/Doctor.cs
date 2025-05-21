using MediCore_Library.Models.Identities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_Library.Models.Entities
{
	public class Doctor
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string HospitalName { get; set; } = string.Empty;
		public string ProfessionalBio { get; set; } = string.Empty;

		public List<Schedule> Schedules { get; set; } = new List<Schedule>();

		[ForeignKey("ApplicationUser")]
		public Guid? UserId { get; set; }
		public ApplicationUser? User { get; set; }
	}
}
