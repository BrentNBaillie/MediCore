namespace MediCore_Library.Models.DTOs.DTO_Entities.Full
{
	public class PrescriptionFullDTO
	{
		public Guid? Id { get; set; }
		public int Quantity { get; set; } = 0;
		public MedicineDTO? Medicine { get; set; }
		public DoctorDTO? Doctor { get; set; }
		public PatientDTO? Patient { get; set; }
		public Guid? BillId { get; set; }
	}
}
