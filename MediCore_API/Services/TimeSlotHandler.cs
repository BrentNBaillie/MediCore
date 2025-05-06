using MediCore_API.Interfaces;
using MediCore_API.Models.Entities;

namespace MediCore_API.Services
{
	public class TimeSlotHandler : ITimeSlotHandler
	{
		public List<TimeSlot> CreateTimeSlots(Schedule schedule)
		{
			try
			{
				TimeOnly start = schedule.Start;
				TimeOnly end = start.AddHours(1);
				int timeSlotCount = (int)(schedule.End.ToTimeSpan() - start.ToTimeSpan()).TotalHours;
				List<TimeSlot> timeSlots = new List<TimeSlot>();

				for (int i = 0; i < timeSlotCount; i++)
				{
					timeSlots.Add(new TimeSlot
					{
						Start = start,
						End = end,
						ScheduleId = schedule.Id
					});
					start = start.AddHours(1);
					end = end.AddHours(1);
				}
				return timeSlots;
			}
			catch (Exception e)
			{
				return new List<TimeSlot>();
			}
		}
	}
}
