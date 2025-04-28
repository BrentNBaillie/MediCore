using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class TimeSlot
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public TimeOnly? Start { get; set; } = null;
		public TimeOnly? End { get; set; } = null;
		public bool IsAvailable { get; set; } = true;

		public Guid ScheduleId { get; set; } = Guid.Empty;
		public Schedule? Schedule { get; set; } = null;

		public Appointment? Appointment { get; set; } = null;
	}
}
