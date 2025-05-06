namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class ScheduleDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public DateOnly Date { get; set; } = new DateOnly();
		public TimeOnly Start { get; set; } = new TimeOnly();
		public TimeOnly End { get; set; } = new TimeOnly();
		public Guid DoctorId { get; set; } = Guid.Empty;
		public List<TimeSlotDTO>? TimeSlots { get; set; } = new List<TimeSlotDTO>();
	}
}
