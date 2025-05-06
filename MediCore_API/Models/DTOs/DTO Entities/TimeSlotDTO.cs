namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class TimeSlotDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public TimeOnly? Start { get; set; } = null;
		public TimeOnly? End { get; set; } = null;
		public bool IsAvailable { get; set; } = true;
		public Guid ScheduleId { get; set; } = Guid.Empty;
	}
}
