using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.DTOs
{
	public class DoctorDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string HospitalName { get; set; } = string.Empty;
		public string ProfessionalBio { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}
