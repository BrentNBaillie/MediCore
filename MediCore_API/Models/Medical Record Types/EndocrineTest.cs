using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediCore_API.Models.Entities;

namespace MediCore_API.Models.Medical_Record_Types
{
	public class EndocrineTest
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; } = DateTime.Now;
		public string Notes { get; set; } = string.Empty;

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? MedicalRecordId { get; set; }
		public MedicalRecord? MedicalRecord { get; set; }

		public bool ThyroidFunctionTest { get; set; } = false;
		public bool CortisolTest { get; set; } = false;
		public bool GrowthHormoneTest { get; set; } = false;
		public bool InsulinTest { get; set; } = false;
		public bool CPeptideTest { get; set; } = false;
		public bool ACTHStimulationTest { get; set; } = false;
		public bool DHEASulfateTest { get; set; } = false;
		public bool TestosteroneTest { get; set; } = false;
		public bool EstrogenTest { get; set; } = false;
		public bool ProgesteroneTest { get; set; } = false;
		public bool ParathyroidHormoneTest { get; set; } = false;
		public bool ProlactinTest { get; set; } = false;

		public double TSH { get; set; } = 0;
		public double FreeT3 { get; set; } = 0;
		public double FreeT4 { get; set; } = 0;
		public double CortisolLevel { get; set; } = 0;
		public double InsulinLevel { get; set; } = 0;
		public double BloodGlucose { get; set; } = 0;
		public double HemoglobinA1c { get; set; } = 0;
		public double DHEASulfate { get; set; } = 0;
		public double Testosterone { get; set; } = 0;
		public double Estrogen { get; set; } = 0;
		public double Progesterone { get; set; } = 0;
		public double Prolactin { get; set; } = 0;
		public double ParathyroidHormone { get; set; } = 0;
		public double CalciumLevel { get; set; } = 0;
	}
}
