using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_API.Models.Entities;

namespace MediCore_API.Models.Medical_Record_Types
{
	public class BodyMeasurement
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid DoctorId { get; set; } = Guid.Empty;
		public Doctor? Doctor { get; set; } = null;

		public Guid MedicalRecordId { get; set; } = Guid.Empty;
		public MedicalRecord? MedicalRecord { get; set; } = null;

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
