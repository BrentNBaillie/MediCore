namespace MediCore_Library.Models.DTOs.DTO_Entities
{
	public class TimeSlotDTO
	{
		public Guid? Id { get; set; }
		public TimeOnly? Start { get; set; }
		public TimeOnly? End { get; set; }
		public bool IsAvailable { get; set; } = true;
		public Guid? ScheduleId { get; set; }
	}
}
