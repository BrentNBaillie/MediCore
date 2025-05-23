using Microsoft.AspNetCore.Identity;


namespace MediCore_Library.Models.Identities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public bool IsLoggedIn { get; set; } = false;
	}
}
