using Microsoft.AspNetCore.Identity;

namespace MediCore_API.Interfaces
{
	public interface IJwtTokenService
	{
		public Task<string> GenerateJwtTokenAsync(IdentityUser user);
	}
}
