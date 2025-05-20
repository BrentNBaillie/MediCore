using System.ComponentModel.DataAnnotations;
using MediCore_Library.Models.Entities;

namespace MediCore_Library.Models.Medical_Record_Types
{
	public class ImagingReport
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? MedicalRecordId { get; set; }
		public MedicalRecord? MedicalRecord { get; set; }

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
