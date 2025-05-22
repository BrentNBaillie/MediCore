using MediCore_API.Interfaces;
using AutoMapper;
using MediCore_API.Data;
using Microsoft.EntityFrameworkCore;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.DTOs.DTO_Entities.Full;
using MediCore_Library.Models.Medical_Record_Types;
using MediCore_Library.Models.DTOs.DTO_Medical_Record_Types;

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
				cfg.CreateMap<MessageDTO, Message>().ForMember(dest => dest.Id, opt => opt.Ignore())
													.ForMember(dest => dest.Date, opt => opt.Ignore());
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

				cfg.CreateMap<Appointment, AppointmentFullDTO>();
				cfg.CreateMap<AppointmentFullDTO, Appointment>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Bill, BillFullDTO>();
				cfg.CreateMap<BillFullDTO, Bill>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Feedback, FeedbackFullDTO>();
				cfg.CreateMap<FeedbackFullDTO, Feedback>().ForMember(dest => dest.Id, opt => opt.Ignore());
				cfg.CreateMap<Patient, PatientFullDTO>();
				cfg.CreateMap<PatientFullDTO, Patient>().ForMember(dest => dest.Id, opt => opt.Ignore());

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

				cfg.CreateMap<MedicalRecord, MedicalRecordFullDTO>();
				cfg.CreateMap<MedicalRecordFullDTO, MedicalRecord>()
				.ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient!.Id))
					.ForMember(dest => dest.Id, opt => opt.Ignore());

				cfg.CreateMap<MedicalRecord, MedicalRecordDTO>()
					.ForMember(dest => dest.AllergyTests, opt => opt.MapFrom(src => src.AllergyTests.Select(x => x.Id)))
					.ForMember(dest => dest.BodyMeasurements, opt => opt.MapFrom(src => src.BodyMeasurements.Select(x => x.Id)))
					.ForMember(dest => dest.CardiacTests, opt => opt.MapFrom(src => src.CardiacTests.Select(x => x.Id)))
					.ForMember(dest => dest.EndocrineTests, opt => opt.MapFrom(src => src.EndocrineTests.Select(x => x.Id)))
					.ForMember(dest => dest.GeneticTests, opt => opt.MapFrom(src => src.GeneticTests.Select(x => x.Id)))
					.ForMember(dest => dest.ImagingReports, opt => opt.MapFrom(src => src.ImagingReports.Select(x => x.Id)))
					.ForMember(dest => dest.InfectiousDiseaseTests, opt => opt.MapFrom(src => src.InfectiousDiseaseTests.Select(x => x.Id)))
					.ForMember(dest => dest.LaboratoryTests, opt => opt.MapFrom(src => src.LaboratoryTests.Select(x => x.Id)))
					.ForMember(dest => dest.NeurologicalTests, opt => opt.MapFrom(src => src.NeurologicalTests.Select(x => x.Id)))
					.ForMember(dest => dest.RespiratoryTests, opt => opt.MapFrom(src => src.RespiratoryTests.Select(x => x.Id)))
					.ForMember(dest => dest.VitalSigns, opt => opt.MapFrom(src => src.VitalSigns.Select(x => x.Id)));

			});

			mapper = new Mapper(config); ;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return mapper.Map<TDestination>(source);
		}

		public async Task<MedicalRecord> MapAsync(MedicalRecordDTO dto, MediCoreContext context)
		{
			MedicalRecord record = new MedicalRecord
			{
				Notes = dto.Notes,
				Date = dto.Date ?? DateTime.Now,
				PatientId = dto.PatientId
			};

			if (dto.AllergyTests is not null)
			{
				record.AllergyTests = await context.AllergyTests.Where(x => dto.AllergyTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.BodyMeasurements is not null)
			{
				record.BodyMeasurements = await context.BodyMeasurements.Where(x => dto.BodyMeasurements.Contains(x.Id)).ToListAsync();
			}
			if (dto.CardiacTests is not null)
			{
				record.CardiacTests = await context.CardiacTests.Where(x => dto.CardiacTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.EndocrineTests is not null)
			{
				record.EndocrineTests = await context.EndocrineTests.Where(x => dto.EndocrineTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.GeneticTests is not null)
			{
				record.GeneticTests = await context.GeneticTests.Where(x => dto.GeneticTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.ImagingReports is not null)
			{
				record.ImagingReports = await context.ImagingReports.Where(x => dto.ImagingReports.Contains(x.Id)).ToListAsync();
			}
			if (dto.InfectiousDiseaseTests is not null)
			{
				record.InfectiousDiseaseTests = await context.InfectiousDiseaseTests.Where(x => dto.InfectiousDiseaseTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.LaboratoryTests is not null)
			{
				record.LaboratoryTests = await context.LaboratoryTests.Where(x => dto.LaboratoryTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.NeurologicalTests is not null)
			{
				record.NeurologicalTests = await context.NeurologicalTests.Where(x => dto.NeurologicalTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.RespiratoryTests is not null)
			{
				record.RespiratoryTests = await context.RespiratoryTests.Where(x => dto.RespiratoryTests.Contains(x.Id)).ToListAsync();
			}
			if (dto.VitalSigns is not null)
			{
				record.VitalSigns = await context.VitalSigns.Where(x => dto.VitalSigns.Contains(x.Id)).ToListAsync();
			}
			return record;
		}
	}
}