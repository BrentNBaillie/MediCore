using MediCore_API.Interfaces;
using AutoMapper;
using MediCore_API.Models.DTOs;
using MediCore_API.Models.Entities;
using MediCore_API.Models.DTOs.DTO_Entities;

namespace MediCore_API.Services
{
	public class ModelMapper : IModelMapper
	{
		private readonly IMapper mapper;

		public ModelMapper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Address, AddressDTO>();
				cfg.CreateMap<AddressDTO, Address>();
				cfg.CreateMap<Appointment, AppointmentDTO>();
				cfg.CreateMap<AppointmentDTO, Appointment>();
				cfg.CreateMap<Bill, BillDTO>().ForMember(dest => dest.Prescriptions, opt => opt.Ignore());
				cfg.CreateMap<BillDTO, Bill>().ForMember(dest => dest.Prescriptions, opt => opt.Ignore());
				cfg.CreateMap<Doctor, DoctorDTO>();
				cfg.CreateMap<DoctorDTO, Doctor>();
				cfg.CreateMap<Feedback, FeedbackDTO>();
				cfg.CreateMap<FeedbackDTO, Feedback>();
				cfg.CreateMap<MedicalRecord, MedicalRecordDTO>();
				cfg.CreateMap<MedicalRecordDTO, MedicalRecord>();
				cfg.CreateMap<Medicine, MedicineDTO>();
				cfg.CreateMap<MedicineDTO, Medicine>();
				cfg.CreateMap<Message, MessageDTO>();
				cfg.CreateMap<MessageDTO, Message>();
				cfg.CreateMap<Patient, PatientDTO>();
				cfg.CreateMap<PatientDTO, Patient>();
				cfg.CreateMap<Prescription, PrescriptionDTO>();
				cfg.CreateMap<PrescriptionDTO, Prescription>();
				cfg.CreateMap<Schedule, ScheduleDTO>();
				cfg.CreateMap<ScheduleDTO, Schedule>();
				cfg.CreateMap<Staff, StaffDTO>();
				cfg.CreateMap<StaffDTO, Staff>();
				cfg.CreateMap<StaffRole, StaffRoleDTO>();
				cfg.CreateMap<StaffRoleDTO, StaffRole>();
				cfg.CreateMap<TimeSlot, TimeSlotDTO>();
				cfg.CreateMap<TimeSlotDTO, TimeSlot>();
			});

			mapper = new Mapper(config); ;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return mapper.Map<TDestination>(source);
		}
	}
}