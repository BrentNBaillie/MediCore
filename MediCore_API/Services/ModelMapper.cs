using MediCore_API.Interfaces;
using AutoMapper;
using MediCore_API.Models.DTOs;
using MediCore_API.Models.Entities;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Medical_Record_Types;
using MediCore_API.Models.DTOs.DTO_Medical_Record_Types;

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
				cfg.CreateMap<Medicine, MedicineDTO>();
				cfg.CreateMap<MedicineDTO, Medicine>();
				cfg.CreateMap<Chat, ChatDTO>();
				cfg.CreateMap<ChatDTO, Chat>();
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

				cfg.CreateMap<AllergyTest, AllergyTestDTO>();
				cfg.CreateMap<AllergyTestDTO, AllergyTest>();
				cfg.CreateMap<BodyMeasurement, BodyMeasurementDTO>();
				cfg.CreateMap<BodyMeasurementDTO, BodyMeasurement>();
				cfg.CreateMap<CardiacTest, CardiacTestDTO>();
				cfg.CreateMap<CardiacTestDTO, CardiacTest>();
				cfg.CreateMap<EndocrineTest, EndocrineTestDTO>();
				cfg.CreateMap<EndocrineTestDTO, EndocrineTest>();
				cfg.CreateMap<GeneticTest, GeneticTestDTO>();
				cfg.CreateMap<GeneticTestDTO, GeneticTest>();
				cfg.CreateMap<ImagingReport, ImagingReportDTO>();
				cfg.CreateMap<ImagingReportDTO, ImagingReport>();
				cfg.CreateMap<InfectiousDiseaseTest, InfectiousDiseaseTestDTO>();
				cfg.CreateMap<InfectiousDiseaseTestDTO, InfectiousDiseaseTest>();
				cfg.CreateMap<LaboratoryTest, LaboratoryTestDTO>();
				cfg.CreateMap<LaboratoryTestDTO, LaboratoryTest>();
				cfg.CreateMap<NeurologicalTest, NeurologicalTestDTO>();
				cfg.CreateMap<NeurologicalTestDTO, NeurologicalTest>();
				cfg.CreateMap<RespiratoryTest, RespiratoryTestDTO>();
				cfg.CreateMap<RespiratoryTestDTO, RespiratoryTest>();
				cfg.CreateMap<VitalSign, VitalSignDTO>();
				cfg.CreateMap<VitalSignDTO, VitalSign>();

				cfg.CreateMap<MedicalRecord, MedicalRecordDTO>();
				cfg.CreateMap<MedicalRecordDTO, MedicalRecord>();
			});

			mapper = new Mapper(config); ;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return mapper.Map<TDestination>(source);
		}
	}
}