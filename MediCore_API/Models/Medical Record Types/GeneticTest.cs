using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_API.Models.Entities;

namespace MediCore_API.Models.Medical_Record_Types
{
	public class GeneticTest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? MedicalRecordId { get; set; }
		public MedicalRecord? MedicalRecord { get; set; }

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
