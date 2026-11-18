using MediCore_Library.Models.Identities;

namespace MediCore_API.Domain.Interfaces.Iservices
{
	public interface IJwtTokenService
	{
		public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
	}
}
