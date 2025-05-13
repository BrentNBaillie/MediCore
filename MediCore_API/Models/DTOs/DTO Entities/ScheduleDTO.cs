namespace MediCore_API.Models.DTOs.DTO_Entities
{
	public class ScheduleDTO
	{
		public Guid? Id { get; set; }
		public DateOnly? Date { get; set; }
		public TimeOnly? Start { get; set; }
		public TimeOnly? End { get; set; }
		public Guid? DoctorId { get; set; }
	}
}
