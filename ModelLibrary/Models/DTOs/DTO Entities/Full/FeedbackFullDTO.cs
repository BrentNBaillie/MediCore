namespace MediCore_Library.Models.DTOs.DTO_Entities.Full
{
	public class FeedbackFullDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Details { get; set; } = string.Empty;
		public PatientDTO? Patient { get; set; }
	}
}
