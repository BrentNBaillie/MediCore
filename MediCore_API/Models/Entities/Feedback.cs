using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediCore_API.Models.Entities
{
	public class Feedback
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime? Date { get; set; } = DateTime.Now;
		public string Details { get; set; } = string.Empty;

		public Guid PatientId { get; set; } = Guid.Empty;
		public Patient? Patient { get; set; } = null;
	}
}
