using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class VitalSignDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public DateTime? Date { get; set; } = null;
		public string Notes { get; set; } = string.Empty;
		public Guid DoctorId { get; set; } = Guid.Empty;
		public Guid MedicalRecordId { get; set; } = Guid.Empty;

		public float HeartRate { get; set; } = 0;
		public float BloodPressureSystolic { get; set; } = 0;
		public float BloodPressureDiastolic { get; set; } = 0;
		public float RespiratoryRate { get; set; } = 0;
		public float Temperature { get; set; } = 0;
		public float OxygenSaturation { get; set; } = 0;
		public string MeasurementLocation { get; set; } = string.Empty;
	}
}
