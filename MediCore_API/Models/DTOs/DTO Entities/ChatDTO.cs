using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class ChatDTO
	{
		public List<MessageDTO>? Messages { get; set; } = null;
		public string[] Ids { get; set; } = new string[2];
		public string[] Names { get; set; } = new string[2];
	}
}
