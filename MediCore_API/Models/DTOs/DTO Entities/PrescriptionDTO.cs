namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class PrescriptionDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public int Quantity { get; set; } = 0;
		public Guid MedicineId { get; set; } = Guid.Empty;
		public Guid DoctorId { get; set; } = Guid.Empty;
		public Guid PatientId { get; set; } = Guid.Empty;
		public Guid BillId { get; set; } = Guid.Empty;
	}
}
