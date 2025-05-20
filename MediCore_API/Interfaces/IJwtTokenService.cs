using MediCore_Library.Models.Identities;

namespace MediCore_API.Interfaces
{
	public interface IJwtTokenService
	{
		public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
	}
}
