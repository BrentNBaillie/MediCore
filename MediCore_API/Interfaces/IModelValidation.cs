using MediCore_API.Models.DTOs;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;

namespace MediCore_API.Interfaces
{
	public interface IModelValidation
	{
		public bool RoleIsValid(StaffRoleDTO role);
		public bool PrescriptionIsValid(PrescriptionDTO prescription);
		public bool FeedbackIsValid(FeedbackDTO feedback);
		public bool BillIsValid(BillDTO bill);
		public bool AppointmentIsValid(AppointmentDTO appointment);
		public bool MedicineIsValid(MedicineDTO medicine);
		public bool AddressIsValid(AddressDTO address);
	}
}
