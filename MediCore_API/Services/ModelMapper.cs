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
				cfg.CreateMap<AddressDTO, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Appointment, AppointmentDTO>();
				cfg.CreateMap<AppointmentDTO, Appointment>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Bill, BillDTO>();
				cfg.CreateMap<BillDTO, Bill>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Prescriptions, opt => opt.Ignore());
				cfg.CreateMap<Doctor, DoctorDTO>();
				cfg.CreateMap<DoctorDTO, Doctor>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Feedback, FeedbackDTO>();
				cfg.CreateMap<FeedbackDTO, Feedback>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Medicine, MedicineDTO>();
				cfg.CreateMap<MedicineDTO, Medicine>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Chat, ChatDTO>();
				cfg.CreateMap<ChatDTO, Chat>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Message, MessageDTO>();
				cfg.CreateMap<MessageDTO, Message>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Patient, PatientDTO>();
				cfg.CreateMap<PatientDTO, Patient>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Prescription, PrescriptionDTO>();
				cfg.CreateMap<PrescriptionDTO, Prescription>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Schedule, ScheduleDTO>();
				cfg.CreateMap<ScheduleDTO, Schedule>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Staff, StaffDTO>();
				cfg.CreateMap<StaffDTO, Staff>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<StaffRole, StaffRoleDTO>();
				cfg.CreateMap<StaffRoleDTO, StaffRole>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<TimeSlot, TimeSlotDTO>();
				cfg.CreateMap<TimeSlotDTO, TimeSlot>().ForMember(dest => dest.Id, opt => opt.Ignore());

				cfg.CreateMap<AllergyTest, AllergyTestDTO>();
				cfg.CreateMap<AllergyTestDTO, AllergyTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<BodyMeasurement, BodyMeasurementDTO>();
				cfg.CreateMap<BodyMeasurementDTO, BodyMeasurement>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<CardiacTest, CardiacTestDTO>();
				cfg.CreateMap<CardiacTestDTO, CardiacTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<EndocrineTest, EndocrineTestDTO>();
				cfg.CreateMap<EndocrineTestDTO, EndocrineTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<GeneticTest, GeneticTestDTO>();
				cfg.CreateMap<GeneticTestDTO, GeneticTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<ImagingReport, ImagingReportDTO>();
				cfg.CreateMap<ImagingReportDTO, ImagingReport>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<InfectiousDiseaseTest, InfectiousDiseaseTestDTO>();
				cfg.CreateMap<InfectiousDiseaseTestDTO, InfectiousDiseaseTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<LaboratoryTest, LaboratoryTestDTO>();
				cfg.CreateMap<LaboratoryTestDTO, LaboratoryTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<NeurologicalTest, NeurologicalTestDTO>();
				cfg.CreateMap<NeurologicalTestDTO, NeurologicalTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<RespiratoryTest, RespiratoryTestDTO>();
				cfg.CreateMap<RespiratoryTestDTO, RespiratoryTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<VitalSign, VitalSignDTO>();
				cfg.CreateMap<VitalSignDTO, VitalSign>().ForMember(dest => dest.Id, opt => opt.Ignore());

				cfg.CreateMap<MedicalRecord, MedicalRecordDTO>();
				cfg.CreateMap<MedicalRecordDTO, MedicalRecord>().ForMember(dest => dest.Id, opt => opt.Ignore());
			});

			mapper = new Mapper(config); ;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return mapper.Map<TDestination>(source);
		}
	}
}