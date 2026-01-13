using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Identities;
using OneOf;

namespace MediCore_Blazor.Services
{
	public class SessionState
	{
		public LoginResponse? LoginResponse { get; set; }
		public OneOf<DoctorDTO, NurseDTO, PatientDTO>? Profile { get; set; }
	}
}
