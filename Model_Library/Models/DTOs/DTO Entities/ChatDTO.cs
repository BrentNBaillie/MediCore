namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class ChatDTO
	{
		public Guid? Id { get; set; }
		public string[] Ids { get; set; } = new string[2];
		public string[] Names { get; set; } = new string[2];
		public List<MessageDTO>? Messages { get; set; }
	}
}
