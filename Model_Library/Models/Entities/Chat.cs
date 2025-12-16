using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Entities
{
	public class Chat
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid[] Ids { get; set; } = new Guid[2];
		public string[] Names { get; set; } = new string[2];
		public List<Message> Messages { get; set; } = new List<Message>();
	}
}
