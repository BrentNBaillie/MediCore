using MediCore_API.Models.DTOs.DTO_Medical_Record_Types;

namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class MedicalRecordDTO
	{
		public Guid? Id { get; set; }
		public string Notes { get; set; } = string.Empty;
		public DateTime? Date { get; set; }
		public Guid? PatientId { get; set; }
		public List<AllergyTestDTO>? AllergyTests { get; set; }
		public List<BodyMeasurementDTO>? BodyMeasurements { get; set; }
		public List<CardiacTestDTO>? CardiacTests { get; set; }
		public List<EndocrineTestDTO>? EndocrineTests { get; set; }
		public List<GeneticTestDTO>? GeneticTests { get; set; }
		public List<ImagingReportDTO>? ImagingReports { get; set; }
		public List<InfectiousDiseaseTestDTO>? InfectiousDiseaseTests { get; set; }
		public List<LaboratoryTestDTO>? LaboratoryTests { get; set; }
		public List<NeurologicalTestDTO>? NeurologicalTests { get; set; }
		public List<RespiratoryTestDTO>? RespiratoryTests { get; set; }
		public List<VitalSignDTO>? VitalSigns { get; set; }
	}
}
