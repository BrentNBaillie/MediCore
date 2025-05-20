namespace MediCore_Library.Models.DTOs.DTO_Medical_Record_Types
{
	public class VitalSignDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public float HeartRate { get; set; } = 0;
		public float BloodPressureSystolic { get; set; } = 0;
		public float BloodPressureDiastolic { get; set; } = 0;
		public float RespiratoryRate { get; set; } = 0;
		public float Temperature { get; set; } = 0;
		public float OxygenSaturation { get; set; } = 0;
		public string MeasurementLocation { get; set; } = string.Empty;
	}
}
