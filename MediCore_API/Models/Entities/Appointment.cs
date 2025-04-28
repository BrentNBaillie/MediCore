using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Appointment
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Status { get; set; } = string.Empty;

		public Guid TimeSlotId { get; set; } = Guid.Empty;
		public TimeSlot? TimeSlot { get; set; } = null;

		public Guid DoctorId { get; set; } = Guid.Empty;
		public Doctor? Doctor { get; set; } = null;

		public Guid PatientId { get; set; } = Guid.Empty;
		public Patient? Patient { get; set; } = null;

		public Guid BillId {  get; set; } = Guid.Empty;
		public Bill Bill { get; set; } = null!;
	}
}
