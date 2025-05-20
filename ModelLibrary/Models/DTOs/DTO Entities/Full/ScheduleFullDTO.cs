namespace MediCore_Library.Models.DTOs.DTO_Entities.Full
{
	public class ScheduleFullDTO
	{
		public Guid? Id { get; set; }
		public DateOnly? Date { get; set; }
		public TimeOnly? Start { get; set; }
		public TimeOnly? End { get; set; }
		public DoctorDTO? Doctor { get; set; }
	}
}
