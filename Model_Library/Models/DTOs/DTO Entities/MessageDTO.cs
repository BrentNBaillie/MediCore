namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class MessageDTO
	{
		public Guid? Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public DateTime? Date { get; set; }
		public string SenderId { get; set; } = string.Empty;
		public Guid? ChatId { get; set; }
	}
}
