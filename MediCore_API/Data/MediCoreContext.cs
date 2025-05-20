using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;
using MediCore_Library.Models.Medical_Record_Types;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Data
{
	public class MediCoreContext :IdentityDbContext<ApplicationUser>
	{
		public MediCoreContext(DbContextOptions<MediCoreContext> options) : base(options) { }

		public DbSet<Bill> Bills { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Staff> StaffMembers { get; set; }
		public DbSet<StaffRole> StaffRoles { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }
		public DbSet<Message> Messages { get; set; }	
		public DbSet<Chat> Chats { get; set; }
		public DbSet<Medicine> Medicines { get; set; }
		public DbSet<MedicalRecord> MedicalRecords { get; set; }

		public DbSet<AllergyTest> AllergyTests { get; set; }
		public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
		public DbSet<CardiacTest> CardiacTests { get; set; }
		public DbSet<EndocrineTest> EndocrineTests { get; set; }
		public DbSet<GeneticTest> GeneticTests { get; set; }
		public DbSet<ImagingReport> ImagingReports { get; set; }
		public DbSet<InfectiousDiseaseTest> InfectiousDiseaseTests { get; set; }
		public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
		public DbSet<NeurologicalTest> NeurologicalTests { get; set; }
		public DbSet<RespiratoryTest> RespiratoryTests { get; set; }
		public DbSet<VitalSign> VitalSigns { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			#region Appointment
			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.TimeSlot)
				.WithOne(ts => ts.Appointment)
				.HasForeignKey<Appointment>(a => a.TimeSlotId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Patient)
				.WithMany()
				.HasForeignKey(a => a.PatientId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Bill)
				.WithOne(b => b.Appointment)
				.HasForeignKey<Appointment>(a => a.BillId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Bill
			modelBuilder.Entity<Bill>()
				.HasMany(b => b.Prescriptions)
				.WithOne()
				.HasForeignKey(p => p.BillId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Doctor
			modelBuilder.Entity<Doctor>()
				.HasMany(d => d.Schedules)
				.WithOne(s => s.Doctor)
				.HasForeignKey(s => s.DoctorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Doctor>()
				.HasOne(d => d.User)
				.WithOne()
				.HasForeignKey<Doctor>(d => d.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Feedback
			modelBuilder.Entity<Feedback>()
				.HasOne(f => f.Patient)
				.WithMany()
				.HasForeignKey(f => f.PatientId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region MedicalRecord
			modelBuilder.Entity<MedicalRecord>()
				.HasOne(f => f.Patient)
				.WithOne()
				.HasForeignKey<MedicalRecord>(mr => mr.PatientId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.AllergyTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.BodyMeasurements)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.CardiacTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.EndocrineTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.GeneticTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.ImagingReports)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.InfectiousDiseaseTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.LaboratoryTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.NeurologicalTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.RespiratoryTests)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<MedicalRecord>()
				.HasMany(mr => mr.VitalSigns)
				.WithOne()
				.HasForeignKey(x => x.MedicalRecordId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Chat
			modelBuilder.Entity<Chat>()
				.HasMany(c => c.Messages)
				.WithOne(m => m.Chat)
				.HasForeignKey(m => m.ChatId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Patient
			modelBuilder.Entity<Patient>()
				.HasOne(p => p.Address)
				.WithMany()
				.HasForeignKey(p => p.AddressId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Patient>()
				.HasOne(p => p.User)
				.WithOne()
				.HasForeignKey<Patient>(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Prescription
			modelBuilder.Entity<Prescription>()
				.HasOne(p => p.Doctor)
				.WithMany()
				.HasForeignKey(p => p.DoctorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Prescription>()
				.HasOne(p => p.Patient)
				.WithMany()
				.HasForeignKey(p => p.PatientId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Prescription>()
				.HasOne(p => p.Medicine)
				.WithMany()
				.HasForeignKey(p => p.MedicineId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Schedule
			modelBuilder.Entity<Schedule>()
				.HasOne(s => s.Doctor)
				.WithMany()
				.HasForeignKey(s => s.DoctorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Schedule>()
				.HasMany(s => s.TimeSlots)
				.WithOne(ts => ts.Schedule)
				.HasForeignKey(ts => ts.ScheduleId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
			#region Staff
			modelBuilder.Entity<Staff>()
				.HasOne(s => s.Role)
				.WithMany()
				.HasForeignKey (s => s.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Staff>()
				.HasOne(s => s.User)
				.WithOne()
				.HasForeignKey<Staff>(s => s.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			#endregion
		}
	}
}
