using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_API.Models.Entities;

namespace MediCore_API.Models.Medical_Record_Types
{
	public class NeurologicalTest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid DoctorId { get; set; } = Guid.Empty;
		public Doctor? Doctor { get; set; } = null;

		public Guid MedicalRecordId { get; set; } = Guid.Empty;
		public MedicalRecord? MedicalRecord { get; set; } = null;

		public bool BabinskiSign { get; set; } = false;
		public bool RombergTest { get; set; } = false;
		public bool FingerNoseTest { get; set; } = false;
		public bool GaitAssessment { get; set; } = false;
		public bool MuscleWeakness { get; set; } = false;
		public bool Tremors { get; set; } = false;

		public bool LightTouchTest { get; set; } = false;
		public bool PainSensationTest { get; set; } = false;
		public bool TemperatureSensationTest { get; set; } = false;
		public bool VibratorySensationTest { get; set; } = false;
		public bool PositionSenseTest { get; set; } = false;

		public bool MiniMentalStateExam { get; set; } = false;
		public int MMSEScore { get; set; } = 0;
		public bool ClockDrawingTest { get; set; } = false;
		public bool TrailMakingTest { get; set; } = false;
		public bool VerbalFluencyTest { get; set; } = false;

		public bool VisionAbnormality { get; set; } = false;
		public bool FacialNerveWeakness { get; set; } = false;
		public bool HearingLoss { get; set; } = false;
		public bool SwallowingDifficulty { get; set; } = false;

		public bool EEGPerformed { get; set; } = false;
		public string EEGResults { get; set; } = string.Empty;
		public bool EMGPerformed { get; set; } = false;
		public string EMGResults { get; set; } = string.Empty;
		public bool NCVPerformed { get; set; } = false;
		public string NCVResults { get; set; } = string.Empty;

		public string TestLab { get; set; } = string.Empty;
	}
}
