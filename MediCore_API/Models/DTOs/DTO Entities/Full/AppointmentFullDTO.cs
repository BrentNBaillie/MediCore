namespace MediCore_API.Models.DTOs.DTO_Entities.Full
{
	public class AppointmentFullDTO
	{
		public Guid? Id { get; set; }
		public string Status { get; set; } = string.Empty;
		public TimeSlotDTO? TimeSlot { get; set; }
		public PatientDTO? Patient { get; set; }
	}
}
