using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.Entities
{
	public class Chat
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public List<Message> Messages { get; set; } = new List<Message>();

		public string[] Ids { get; set; } = new string[2];
		public string[] Names { get; set; } = new string[2];
	}
}
