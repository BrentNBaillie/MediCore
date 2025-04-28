using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Prescription
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public int Quantity { get; set; } = 0;

		public Guid MedicineId { get; set; } = Guid.Empty;
		public Medicine Medicine { get; set; } = null!;

		public Guid DoctorId { get; set; } = Guid.Empty;
		public Doctor Doctor { get; set; } = null!;

		public Guid PatientId {  get; set; } = Guid.Empty;
		public Patient Patient { get; set; } = null!;

		public Guid BillId {  get; set; } = Guid.Empty;
	}
}
