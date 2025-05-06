namespace MediCore_API.Models.DTOs.DTO_Entities
{
    public class AppointmentDTO
    {
		public Guid Id { get; set; } = Guid.Empty;
		public string Status {  get; set; } = string.Empty;
        public TimeSlotDTO? TimeSlot { get; set; } = null;
        public PatientDTO? Patient { get; set; } = null;
        public DoctorDTO? Doctor { get; set; } = null;
    }
}
