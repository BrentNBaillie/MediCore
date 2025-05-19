using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Prescription
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public int Quantity { get; set; } = 0;

		public Guid? MedicineId { get; set; }
		public Medicine? Medicine { get; set; }

		public Guid? DoctorId { get; set; }
		public Doctor? Doctor { get; set; }

		public Guid? PatientId {  get; set; }
		public Patient? Patient { get; set; }

		public Guid? BillId {  get; set; }
	}
}
