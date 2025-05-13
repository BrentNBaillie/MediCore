using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class ImagingReportDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public bool XRay { get; set; } = false;
		public bool CTScan { get; set; } = false;
		public bool MRI { get; set; } = false;
		public bool Ultrasound { get; set; } = false;
		public bool Mammogram { get; set; } = false;
		public bool PETScan { get; set; } = false;
		public bool BoneDensityScan { get; set; } = false;
		public bool Echocardiogram { get; set; } = false;
		public bool DopplerUltrasound { get; set; } = false;

		public string GeneralFindings { get; set; } = string.Empty;
		public string Impression { get; set; } = string.Empty;
		public string RadiologistNotes { get; set; } = string.Empty;

		public double TumorSize { get; set; } = 0;
		public double AneurysmDiameter { get; set; } = 0;
		public double BoneDensity { get; set; } = 0;
		public double EjectionFraction { get; set; } = 0;
	}
}
