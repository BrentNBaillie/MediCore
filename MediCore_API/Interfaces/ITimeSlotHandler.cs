using MediCore_Library.Models.Entities;

namespace MediCore_API.Interfaces
{
	public interface ITimeSlotHandler
	{
		public List<TimeSlot> CreateTimeSlots(Schedule schedule);
	}
}
