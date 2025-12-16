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
	public class ModelMapper : Profile
	{
        public ModelMapper()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<AppointmentDTO, Appointment>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Bill, BillDTO>();
            CreateMap<BillDTO, Bill>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Prescriptions, opt => opt.Ignore());
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<DoctorDTO, Doctor>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<FeedbackDTO, Feedback>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Medicine, MedicineDTO>();
            CreateMap<MedicineDTO, Medicine>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Chat, ChatDTO>();
            CreateMap<ChatDTO, Chat>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, Message>().ForMember(dest => dest.Id, opt => opt.Ignore())
                                                .ForMember(dest => dest.Date, opt => opt.Ignore());
            CreateMap<Patient, PatientDTO>();
            CreateMap<PatientDTO, Patient>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Prescription, PrescriptionDTO>();
            CreateMap<PrescriptionDTO, Prescription>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<ScheduleDTO, Schedule>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Nurse, NurseDTO>();
            CreateMap<NurseDTO, Nurse>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TimeSlot, TimeSlotDTO>();
            CreateMap<TimeSlotDTO, TimeSlot>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Appointment, AppointmentFullDTO>();
            CreateMap<AppointmentFullDTO, Appointment>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Bill, BillFullDTO>();
            CreateMap<BillFullDTO, Bill>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Feedback, FeedbackFullDTO>();
            CreateMap<FeedbackFullDTO, Feedback>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Patient, PatientFullDTO>();
            CreateMap<PatientFullDTO, Patient>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AllergyTest, AllergyTestDTO>();
            CreateMap<AllergyTestDTO, AllergyTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<BodyMeasurement, BodyMeasurementDTO>();
            CreateMap<BodyMeasurementDTO, BodyMeasurement>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CardiacTest, CardiacTestDTO>();
            CreateMap<CardiacTestDTO, CardiacTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EndocrineTest, EndocrineTestDTO>();
            CreateMap<EndocrineTestDTO, EndocrineTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<GeneticTest, GeneticTestDTO>();
            CreateMap<GeneticTestDTO, GeneticTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ImagingReport, ImagingReportDTO>();
            CreateMap<ImagingReportDTO, ImagingReport>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<InfectiousDiseaseTest, InfectiousDiseaseTestDTO>();
            CreateMap<InfectiousDiseaseTestDTO, InfectiousDiseaseTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<LaboratoryTest, LaboratoryTestDTO>();
            CreateMap<LaboratoryTestDTO, LaboratoryTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<NeurologicalTest, NeurologicalTestDTO>();
            CreateMap<NeurologicalTestDTO, NeurologicalTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<RespiratoryTest, RespiratoryTestDTO>();
            CreateMap<RespiratoryTestDTO, RespiratoryTest>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<VitalSign, VitalSignDTO>();
            CreateMap<VitalSignDTO, VitalSign>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<MedicalRecord, MedicalRecordFullDTO>();
            CreateMap<MedicalRecordFullDTO, MedicalRecord>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient!.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<MedicalRecord, MedicalRecordDTO>()
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
        }
	}
}