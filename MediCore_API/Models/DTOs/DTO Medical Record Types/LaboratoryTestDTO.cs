using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class LaboratoryTestDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public string TestLab { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public double Hemoglobin { get; set; } = 0;
		public double WhiteBloodCellCount { get; set; } = 0;
		public double PlateletCount { get; set; } = 0;
		public double RedBloodCellCount { get; set; } = 0;
		public double Glucose { get; set; } = 0;
		public double Sodium { get; set; } = 0;
		public double Potassium { get; set; } = 0;
		public double Calcium { get; set; } = 0;
		public double Cholesterol { get; set; } = 0;
		public double ALT { get; set; } = 0;
		public double AST { get; set; } = 0;
		public double Bilirubin { get; set; } = 0;
		public double Creatinine { get; set; } = 0;
		public double BloodUreaNitrogen { get; set; } = 0;
		public double GFR { get; set; } = 0;

		public bool UrineProtein { get; set; } = false;
		public bool UrineGlucose { get; set; } = false;
		public bool UrineKetones { get; set; } = false;
	}
}
