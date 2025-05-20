namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class MedicalRecordDTO
	{
		public Guid? Id { get; set; }
		public string Notes { get; set; } = string.Empty;
		public DateTime? Date { get; set; }
		public Guid? PatientId { get; set; }
		public List<Guid>? AllergyTests { get; set; }
		public List<Guid>? BodyMeasurements { get; set; }
		public List<Guid>? CardiacTests { get; set; }
		public List<Guid>? EndocrineTests { get; set; }
		public List<Guid>? GeneticTests { get; set; }
		public List<Guid>? ImagingReports { get; set; }
		public List<Guid>? InfectiousDiseaseTests { get; set; }
		public List<Guid>? LaboratoryTests { get; set; }
		public List<Guid>? NeurologicalTests { get; set; }
		public List<Guid>? RespiratoryTests { get; set; }
		public List<Guid>? VitalSigns { get; set; }
	}
}
