using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class ScheduleDTO
	{
		public DateOnly Date { get; set; } = new DateOnly();
		public TimeOnly Start { get; set; } = new TimeOnly();
		public TimeOnly End { get; set; } = new TimeOnly();
		public Guid DoctorId { get; set; } = Guid.Empty;
	}
}
