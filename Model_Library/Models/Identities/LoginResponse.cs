namespace MediCore_Library.Models.Identities
{
	public class LoginResponse
	{
		public string Token { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public Guid? UserId { get; set; }
		public Guid? TypeId { get; set; }
	}
}
