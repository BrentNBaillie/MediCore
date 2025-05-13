namespace MediCore_API.Models.Identities
{
	public class LoginResponse
	{
		public string Token { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public Guid? Id { get; set; }
	}
}
