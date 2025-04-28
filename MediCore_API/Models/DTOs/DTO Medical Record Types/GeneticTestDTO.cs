using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs.DTO_Medical_Record_Types
{
	public class GeneticTestDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public DateTime? Date { get; set; } = null;
		public string Notes { get; set; } = string.Empty;
		public Guid DoctorId { get; set; } = Guid.Empty;
		public Guid MedicalRecordId { get; set; } = Guid.Empty;

		public bool CarrierScreening { get; set; } = false;
		public bool WholeExomeSequencing { get; set; } = false;
		public bool WholeGenomeSequencing { get; set; } = false;
		public bool PharmacogeneticTest { get; set; } = false;
		public bool CancerGeneticTest { get; set; } = false;
		public bool CardiovascularGeneticTest { get; set; } = false;
		public bool NeurologicalGeneticTest { get; set; } = false;
		public bool RareDiseaseTest { get; set; } = false;

		public string BRCA1Mutation { get; set; } = string.Empty;
		public string BRCA2Mutation { get; set; } = string.Empty;
		public string MTHFRMutation { get; set; } = string.Empty;
		public string APCMutation { get; set; } = string.Empty;
		public string LRRK2Mutation { get; set; } = string.Empty;
		public string CFTRMutation { get; set; } = string.Empty;
		public string HBBMutation { get; set; } = string.Empty;
		public string HTTMutation { get; set; } = string.Empty;
		public string LDLRMutation { get; set; } = string.Empty;

		public double DiabetesRiskScore { get; set; } = 0;
		public double HeartDiseaseRiskScore { get; set; } = 0;
		public double AlzheimerRiskScore { get; set; } = 0;
		public double ObesityRiskScore { get; set; } = 0;
	}
}
