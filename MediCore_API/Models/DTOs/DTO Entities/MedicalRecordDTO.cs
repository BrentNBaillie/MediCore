using MediCore_API.Models.DTOs.DTO_Medical_Record_Types;
using MediCore_API.Models.Entities;
using MediCore_API.Models.Medical_Record_Types;

namespace MediCore_API.Models.DTOs
{
	public class MedicalRecordDTO
	{
		public string Notes { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public Guid PatientId { get; set; } = Guid.Empty;
		public List<AllergyTestDTO>? AllergyTests { get; set; } = null;
		public List<BodyMeasurementDTO>? BodyMeasurements { get; set; } = null;
		public List<CardiacTestDTO>? CardiacTests { get; set; } = null;
		public List<EndocrineTestDTO>? EndocrineTests { get; set; } = null;
		public List<GeneticTestDTO>? GeneticTests { get; set; } = null;
		public List<ImagingReportDTO>? ImagingReports { get; set; } = null;
		public List<InfectiousDiseaseTestDTO>? InfectiousDiseaseTests { get; set; } = null;
		public List<LaboratoryTestDTO>? LaboratoryTests { get; set; } = null;
		public List<NeurologicalTestDTO>? NeurologicalTests { get; set; } = null;
		public List<RespiratoryTestDTO>? RespiratoryTests { get; set; } = null;
		public List<VitalSignDTO>? VitalSigns { get; set; } = null;
	}
}
