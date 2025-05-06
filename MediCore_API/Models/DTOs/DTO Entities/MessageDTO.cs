using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.DTOs
{
	public class MessageDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public string Text { get; set; } = string.Empty;
		public DateTime? Date { get; set; } = null;
		public string SenderId { get; set; } = string.Empty;
		public Guid ChatId {  get; set; } = Guid.Empty;
	}
}
