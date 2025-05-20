namespace MediCore_Library.Models.DTOs.DTO_Medical_Record_Types
{
	public class InfectiousDiseaseTestDTO
	{
		public Guid? Id { get; set; }
		public DateTime? Date { get; set; }
		public string Notes { get; set; } = string.Empty;
		public Guid? DoctorId { get; set; }
		public Guid? MedicalRecordId { get; set; }

		public bool Covid19 { get; set; } = false;
		public bool Influenza { get; set; } = false;
		public bool Tuberculosis { get; set; } = false;
		public bool HepatitisB { get; set; } = false;
		public bool HepatitisC { get; set; } = false;
		public bool HIV { get; set; } = false;
		public bool Syphilis { get; set; } = false;
		public bool Malaria { get; set; } = false;
		public bool Dengue { get; set; } = false;
		public bool LymeDisease { get; set; } = false;
		public bool ZikaVirus { get; set; } = false;
		public bool EpsteinBarrVirus { get; set; } = false;
		public bool Chlamydia { get; set; } = false;
		public bool Gonorrhea { get; set; } = false;
		public bool MRSA { get; set; } = false;

		public string TestMethod { get; set; } = string.Empty;
		public string Result { get; set; } = string.Empty;
	}
}
