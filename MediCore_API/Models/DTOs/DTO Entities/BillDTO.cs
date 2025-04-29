using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class BillDTO
	{
		public float Amount { get; set; } = 0f;
		public string PaymentMethod { get; set; } = string.Empty;
		public DateTime? Date { get; set; } = null;
		public Guid PatientId { get; set; } = Guid.Empty;
		public Guid AppointmentId { get; set; } = Guid.Empty;
		public List<Guid> Prescriptions { get; set; } = new List<Guid>();
	}
}
