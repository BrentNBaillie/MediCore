using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Identities
{
	public class Login
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
