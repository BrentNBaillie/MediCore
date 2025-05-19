using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Message
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Text { get; set; } = string.Empty;
		public DateTime? Date { get; set; } = DateTime.Now;
		public string SenderId { get; set; } = string.Empty;

		public Guid? ChatId { get; set; }
		public Chat? Chat { get; set; }
	}
}
