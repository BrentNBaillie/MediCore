using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class MedicalRecordDTO
	{
		public string Notes { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public Guid PatientId { get; set; } = Guid.Empty;
	}
}
