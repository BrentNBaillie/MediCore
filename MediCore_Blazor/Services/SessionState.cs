using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;

namespace MediCore_Blazor.Services
{
	public class SessionState
	{
		public LoginResponse? LoginResponse { get; set; }
		public object? Profile { get; set; }
	}
}
