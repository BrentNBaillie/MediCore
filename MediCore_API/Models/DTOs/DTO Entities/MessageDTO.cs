using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediCore_API.Models.DTOs
{
	public class MessageDTO
	{
		public string Text { get; set; } = string.Empty;
		public string SenderId { get; set; } = string.Empty;
		public string RecieverId { get; set; } = string.Empty;
	}
}
