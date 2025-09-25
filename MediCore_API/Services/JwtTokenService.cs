using MediCore_API.Interfaces;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediCore_API.Services
{
	internal sealed class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration config;
		private readonly UserManager<ApplicationUser> userManager;

		public JwtTokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
		{
			this.config = config;
			this.userManager = userManager;
		}
		public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
		{
			var roles = await userManager.GetRolesAsync(user);
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var token = new JwtSecurityToken
			(
				issuer: config["JwtSettings:Issuer"],
				audience: config["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["JwtSettings:ExpireMinutes"])),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
