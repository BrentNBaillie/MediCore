using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.Entities
{
	public class Message
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Text { get; set; } = string.Empty;

		[ForeignKey("ApplicationUser")]
		public string SenderId { get; set; } = string.Empty;
		public IdentityUser? Sender { get; set; } = null;

		[ForeignKey("ApplicationUser")]
		public string RecieverId { get; set; } = string.Empty;
		public IdentityUser? Reciever { get; set; } = null;
	}
}
