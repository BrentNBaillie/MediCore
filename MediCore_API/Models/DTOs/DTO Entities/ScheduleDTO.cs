using MediCore_API.Models.Entities;

namespace MediCore_API.Models.DTOs
{
	public class ScheduleDTO
	{
		public Guid Id { get; set; } = Guid.Empty;
		public DateOnly? Date { get; set; } = null;
		public TimeOnly? Start { get; set; } = null;
		public TimeOnly? End { get; set; } = null;
		public Guid DoctorId { get; set; } = Guid.Empty;
	}
}
