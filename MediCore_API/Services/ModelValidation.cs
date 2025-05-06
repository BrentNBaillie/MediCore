using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;

namespace MediCore_API.Services
{
	public class ModelValidation : IModelValidation
	{
		public bool RoleIsValid(StaffRoleDTO role)
		{
			if (string.IsNullOrEmpty(role.Title)) return false;
			if (string.IsNullOrEmpty(role.Description)) return false;
			return true;
		}

		public bool PrescriptionIsValid(PrescriptionDTO prescription)
		{
			if (prescription.Quantity <= 0) return false;
			if (prescription.MedicineId == Guid.Empty) return false;
			if (prescription.DoctorId == Guid.Empty) return false;
			if (prescription.PatientId == Guid.Empty) return false;
			return true;
		}

		public bool FeedbackIsValid(FeedbackDTO feedback)
		{
			if (string.IsNullOrEmpty(feedback.Details)) return false;
			if (feedback.PatientId == Guid.Empty) return false;
			return true;
		}

		public bool BillIsValid(BillDTO bill)
		{
			if (bill.Amount <= 0) return false;
			if (string.IsNullOrEmpty(bill.PaymentMethod)) return false;
			if (bill.Date is null || bill.Date <= new DateTime(2025, 1, 1)) return false;
			if (bill.PatientId == Guid.Empty) return false;
			if (bill.AppointmentId == Guid.Empty) return false;
			if (!bill.Prescriptions.Any()) return false;
			return true;
		}

		public bool AppointmentIsValid(AppointmentDTO appointment)
		{
			if (string.IsNullOrEmpty(appointment.Status)) return false;
			if (appointment.TimeSlot!.Id == Guid.Empty) return false;
			if (appointment.Patient!.Id == Guid.Empty) return false;
			if (appointment.Doctor!.Id == Guid.Empty) return false;
			return true;
		}
	}
}
