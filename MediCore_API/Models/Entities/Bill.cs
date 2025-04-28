using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Bill
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public float Amount { get; set; } = 0f;
		public string PaymentMethod { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;

		public Guid PatientId { get; set; } = Guid.Empty;
		public Patient? Patient { get; set; } = null;

		public Guid AppointmentId { get; set; } = Guid.Empty;
		public Appointment Appointment { get; set; } = null!;

		public List<Prescription> Prescriptions { get; set; } = new List<Prescription>();
	}
}
