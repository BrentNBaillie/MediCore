namespace MediCore_Library.Models.DTOs.DTO_Entities
{
    public class AppointmentDTO
    {
		public Guid? Id { get; set; }
		public string Status {  get; set; } = string.Empty;
        public Guid? TimeSlotId { get; set; }
        public Guid? PatientId { get; set; }
    }
}
