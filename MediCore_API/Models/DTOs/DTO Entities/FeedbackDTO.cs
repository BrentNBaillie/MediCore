namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class FeedbackDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public DateTime? Date { get; set; } = null;
		public string Details { get; set; } = string.Empty;
		public Guid PatientId { get; set; } = Guid.Empty;
	}
}
