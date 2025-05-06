using MediCore_API.Models.Entities;

namespace MediCore_API.Interfaces
{
	public interface ITimeSlotHandler
	{
		public List<TimeSlot> CreateTimeSlots(Schedule schedule);
	}
}
