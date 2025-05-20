namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class PrescriptionDTO
	{
		public Guid? Id { get; set; }
		public int Quantity { get; set; } = 0;
		public Guid? MedicineId { get; set; }
		public Guid? DoctorId { get; set; }
		public Guid? PatientId { get; set; }
		public Guid? BillId { get; set; }
	}
}
