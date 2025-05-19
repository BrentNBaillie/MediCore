using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class BodyMeasurementDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public double Height { get; set; } = 0;
		public double Weight { get; set; } = 0;
		public double ChestCircumference { get; set; } = 0;
		public double WaistCircumference { get; set; } = 0;
		public double HipCircumference { get; set; } = 0;
		public double NeckCircumference { get; set; } = 0;
		public double UpperArmCircumference { get; set; } = 0;
		public double ForearmCircumference { get; set; } = 0;
		public double ThighCircumference { get; set; } = 0;
		public double CalfCircumference { get; set; } = 0;
		public double BodyFatPercentage { get; set; } = 0;
		public double MuscleMass { get; set; } = 0;
	}
}
