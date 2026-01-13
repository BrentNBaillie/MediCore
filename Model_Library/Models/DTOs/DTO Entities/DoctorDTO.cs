namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class DoctorDTO
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Specialization { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string HospitalName { get; set; } = string.Empty;
		public string ProfessionalBio { get; set; } = string.Empty;
        public double Salary { get; set; } = 0;
        public Guid? UserId { get; set; }
	}
}
