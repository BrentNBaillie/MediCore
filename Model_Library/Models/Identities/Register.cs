using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.DTOs.DTO_Entities.Full;
using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Identities
{
	public class Register
	{
		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required, Length(8,20)]
		public string Password { get; set; } = string.Empty;

		public DoctorDTO? Doctor { get; set; }
		public StaffFullDTO? Staff { get; set; }
		public PatientFullDTO? Patient { get; set; }
	}
}
