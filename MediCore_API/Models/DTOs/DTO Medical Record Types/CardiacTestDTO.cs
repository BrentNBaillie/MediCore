using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class CardiacTestDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public bool Electrocardiogram { get; set; } = false;
		public bool Echocardiogram { get; set; } = false;
		public bool StressTest { get; set; } = false;
		public bool HolterMonitor { get; set; } = false;
		public bool EventMonitor { get; set; } = false;
		public bool CardiacMRI { get; set; } = false;
		public bool CardiacCT { get; set; } = false;
		public bool CoronaryAngiogram { get; set; } = false;
		public bool CalciumScoreTest { get; set; } = false;
		public bool BloodPressureMonitoring { get; set; } = false;

		public int RestingHeartRate { get; set; } = 0;
		public int MaxHeartRate { get; set; } = 0;

		public double SystolicBP { get; set; } = 0;
		public double DiastolicBP { get; set; } = 0;
		public double CholesterolLevel { get; set; } = 0;
		public double TriglycerideLevel { get; set; } = 0;
		public double LDLCholesterol { get; set; } = 0;
		public double HDLCholesterol { get; set; } = 0;
		public double EjectionFraction { get; set; } = 0;
		public double CoronaryCalciumScore { get; set; } = 0;
	}
}
