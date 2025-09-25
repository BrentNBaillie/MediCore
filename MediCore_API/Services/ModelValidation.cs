using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;

namespace MediCore_API.Services
{
	internal sealed class ModelValidation : IModelValidation
	{
		public bool PrescriptionIsValid(PrescriptionDTO prescription)
		{
			if (prescription is null) return false;
			if (prescription.Quantity <= 0) return false;
			if (prescription.MedicineId == Guid.Empty) return false;
			if (prescription.DoctorId == Guid.Empty) return false;
			if (prescription.PatientId == Guid.Empty) return false;
			return true;
		}

		public bool FeedbackIsValid(FeedbackDTO feedback)
		{
			if (feedback is null) return false;
			if (string.IsNullOrEmpty(feedback.Details)) return false;
			if (feedback.PatientId == Guid.Empty) return false;
			return true;
		}

		public bool BillIsValid(BillDTO bill)
		{
			if (bill is null) return false;
			if (bill.Amount <= 0) return false;
			if (string.IsNullOrEmpty(bill.PaymentMethod)) return false;
			if (bill.Date is null || bill.Date <= new DateTime(2025, 1, 1)) return false;
			if (bill.AppointmentId == Guid.Empty) return false;
			if (!bill.Prescriptions!.Any()) return false;
			return true;
		}

		public bool AppointmentIsValid(AppointmentDTO appointment)
		{
			if (appointment is null) return false;
			if (string.IsNullOrEmpty(appointment.Status)) return false;
			if (appointment.TimeSlotId == Guid.Empty) return false;
			if (appointment.TimeSlotId == Guid.Empty) return false;
			if (appointment.TimeSlotId == Guid.Empty) return false;
			return true;
		}

		public bool MedicineIsValid(MedicineDTO medicine)
		{
			if (medicine is null) return false;
			if (string.IsNullOrEmpty(medicine.Name)) return false;	
			if (string.IsNullOrEmpty(medicine.Description)) return false;
			if (medicine.Price <= 0) return false;
			if (string.IsNullOrEmpty(medicine.Manufacturer)) return false;
			return true;
		}

		public bool AddressIsValid(AddressDTO address)
		{
			if (address is null) return false;
			if (string.IsNullOrEmpty(address.Street)) return false;
			if (string.IsNullOrEmpty(address.City)) return false;
			if (string.IsNullOrEmpty(address.ProvinceOrState)) return false;
			if (string.IsNullOrEmpty(address.Country)) return false;
			if (string.IsNullOrEmpty(address.PostalCode)) return false;
			return true;
		}
	}
}
