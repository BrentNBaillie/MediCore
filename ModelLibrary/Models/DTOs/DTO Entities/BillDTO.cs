namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class BillDTO
	{
		public Guid? Id { get; set; }
		public float Amount { get; set; } = 0f;
		public string PaymentMethod { get; set; } = string.Empty;
		public DateTime? Date { get; set; }
		public Guid? AppointmentId { get; set; }
		public List<Guid>? Prescriptions { get; set; }
	}
}
