using MediCore_Library.Models.Entities;

namespace MediCore_API.Domain.Interfaces.Iservices
{
	public interface ITimeSlotHandler
	{
		public List<TimeSlot> CreateTimeSlots(Schedule schedule);
	}
}
