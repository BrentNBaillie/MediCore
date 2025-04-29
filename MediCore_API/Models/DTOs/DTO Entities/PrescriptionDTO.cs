using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class PrescriptionDTO
	{
		public int Quantity { get; set; } = 0;
		public Guid MedicineId { get; set; } = Guid.Empty;
		public Guid DoctorId { get; set; } = Guid.Empty;
		public Guid PatientId { get; set; } = Guid.Empty;
		public Guid BillId { get; set; } = Guid.Empty;
	}
}
