using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class TimeSlotDTO
	{
		public TimeOnly? Start { get; set; } = null;
		public TimeOnly? End { get; set; } = null;
		public bool IsAvailable { get; set; } = true;
		public Guid ScheduleId { get; set; } = Guid.Empty;
	}
}
