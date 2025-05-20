using System.ComponentModel.DataAnnotations;

namespace MediCore_Library.Models.Entities
{
	public class Appointment
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Status { get; set; } = string.Empty;

		public Guid? TimeSlotId { get; set; }
		public TimeSlot? TimeSlot { get; set; }

		public Guid? PatientId { get; set; }
		public Patient? Patient { get; set; }

		public Guid? BillId {  get; set; }
		public Bill? Bill { get; set; }
	}
}
