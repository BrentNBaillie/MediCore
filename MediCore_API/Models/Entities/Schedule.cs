using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Schedule
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateOnly? Date {  get; set; } = DateOnly.FromDateTime(DateTime.Today);
		public TimeOnly? Start {  get; set; } = new TimeOnly(6,0);
		public TimeOnly? End { get; set; } = new TimeOnly(21,0);

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public List<TimeSlot>? TimeSlots { get; set; } = new List<TimeSlot>();
	}
}
