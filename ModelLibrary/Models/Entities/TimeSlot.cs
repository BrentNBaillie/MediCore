using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class TimeSlot
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public TimeOnly? Start { get; set; }
		public TimeOnly? End { get; set; }
		public bool IsAvailable { get; set; } = true;

		public Guid? ScheduleId { get; set; }
		public Schedule? Schedule { get; set; }

		public Appointment? Appointment { get; set; }
	}
}
