using System.ComponentModel.DataAnnotations;
using MediCore_Library.Models.Entities;

namespace MediCore_Library.Models.Medical_Record_Types
{
	public class CardiacTest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? MedicalRecordId { get; set; }
		public MedicalRecord? MedicalRecord { get; set; }

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
