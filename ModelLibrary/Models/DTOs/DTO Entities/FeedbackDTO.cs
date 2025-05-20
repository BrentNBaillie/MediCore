namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class FeedbackDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Details { get; set; } = string.Empty;
		public Guid? PatientId { get; set; }
	}
}
