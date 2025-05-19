using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class RespiratoryTestDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public bool SpirometryPerformed { get; set; } = false;
		public float FEV1 { get; set; } = 0;
		public float FVC { get; set; } = 0;
		public float FEV1FVC { get; set; } = 0;

		public bool PeakFlowTest { get; set; } = false;
		public float PeakFlowRate { get; set; } = 0;

		public bool BloodGasTest { get; set; } = false;
		public float OxygenSaturation { get; set; } = 0;
		public float ArterialPaO2 { get; set; } = 0;
		public float ArterialPaCO2 { get; set; } = 0;
		public float BloodPH { get; set; } = 0;

		public bool ChestXRayPerformed { get; set; } = false;
		public string ChestXRayResults { get; set; } = string.Empty;

		public bool CTScanPerformed { get; set; } = false;
		public string CTScanResults { get; set; } = string.Empty;

		public bool BronchoscopyPerformed { get; set; } = false;
		public string BronchoscopyResults { get; set; } = string.Empty;

		public string TestLab { get; set; } = string.Empty;
	}
}
