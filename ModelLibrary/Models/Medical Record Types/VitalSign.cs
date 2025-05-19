using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_API.Models.Entities;

namespace MediCore_API.Models.Medical_Record_Types
{
	public class VitalSign
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? MedicalRecordId { get; set; }
		public MedicalRecord? MedicalRecord { get; set; }

		public float HeartRate { get; set; } = 0;
		public float BloodPressureSystolic { get; set; } = 0;
		public float BloodPressureDiastolic { get; set; } = 0;
		public float RespiratoryRate { get; set; } = 0;
		public float Temperature { get; set; } = 0;
		public float OxygenSaturation { get; set; } = 0;
		public string MeasurementLocation { get; set; } = string.Empty;
	}
}
