namespace MediCore_API.Models.DTOs
{
    public class AppointmentDTO
    {
		public string Status {  get; set; } = string.Empty;
        public Guid TimeSlotId { get; set; } = Guid.Empty;
        public Guid PatientId { get; set; } = Guid.Empty;
        public Guid DoctorId { get; set; } = Guid.Empty;
    }
}
