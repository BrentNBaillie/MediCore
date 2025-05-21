using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Entities
{
	public class Message
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Text { get; set; } = string.Empty;
		public DateTime? Date { get; set; } = DateTime.Now;
		public Guid? SenderId { get; set; }

		public Guid? ChatId { get; set; }
		public Chat? Chat { get; set; }
	}
}
