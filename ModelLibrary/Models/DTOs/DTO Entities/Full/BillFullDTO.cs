namespace MediCore_API.Models.DTOs.DTO_Entities.Full
{
	public class BillFullDTO
	{
		public Guid? Id { get; set; }
		public float Amount { get; set; } = 0f;
		public string PaymentMethod { get; set; } = string.Empty;
		public DateTime? Date { get; set; }
		public AppointmentDTO? Appointment { get; set; }
		public List<PrescriptionFullDTO>? Prescriptions { get; set; }
	}
}
