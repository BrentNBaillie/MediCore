using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediCore_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    ProvinceOrState = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ids = table.Column<Guid[]>(type: "uuid[]", nullable: false),
                    Names = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Specialization = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    HospitalName = table.Column<string>(type: "text", nullable: false),
                    ProfessionalBio = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Start = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    End = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DoctorId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Doctors_DoctorId1",
                        column: x => x.DoctorId1,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true),
                    BillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    End = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllergyTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Peanut = table.Column<bool>(type: "boolean", nullable: false),
                    TreeNut = table.Column<bool>(type: "boolean", nullable: false),
                    Dairy = table.Column<bool>(type: "boolean", nullable: false),
                    Egg = table.Column<bool>(type: "boolean", nullable: false),
                    Wheat = table.Column<bool>(type: "boolean", nullable: false),
                    Soy = table.Column<bool>(type: "boolean", nullable: false),
                    Fish = table.Column<bool>(type: "boolean", nullable: false),
                    Shellfish = table.Column<bool>(type: "boolean", nullable: false),
                    Pollen = table.Column<bool>(type: "boolean", nullable: false),
                    DustMites = table.Column<bool>(type: "boolean", nullable: false),
                    Mold = table.Column<bool>(type: "boolean", nullable: false),
                    PetDander = table.Column<bool>(type: "boolean", nullable: false),
                    InsectStings = table.Column<bool>(type: "boolean", nullable: false),
                    Penicillin = table.Column<bool>(type: "boolean", nullable: false),
                    Aspirin = table.Column<bool>(type: "boolean", nullable: false),
                    NSAIDs = table.Column<bool>(type: "boolean", nullable: false),
                    SulfaDrugs = table.Column<bool>(type: "boolean", nullable: false),
                    Latex = table.Column<bool>(type: "boolean", nullable: false),
                    Fragrances = table.Column<bool>(type: "boolean", nullable: false),
                    Nickel = table.Column<bool>(type: "boolean", nullable: false),
                    Preservatives = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergyTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllergyTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllergyTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    ChestCircumference = table.Column<double>(type: "double precision", nullable: false),
                    WaistCircumference = table.Column<double>(type: "double precision", nullable: false),
                    HipCircumference = table.Column<double>(type: "double precision", nullable: false),
                    NeckCircumference = table.Column<double>(type: "double precision", nullable: false),
                    UpperArmCircumference = table.Column<double>(type: "double precision", nullable: false),
                    ForearmCircumference = table.Column<double>(type: "double precision", nullable: false),
                    ThighCircumference = table.Column<double>(type: "double precision", nullable: false),
                    CalfCircumference = table.Column<double>(type: "double precision", nullable: false),
                    BodyFatPercentage = table.Column<double>(type: "double precision", nullable: false),
                    MuscleMass = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CardiacTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Electrocardiogram = table.Column<bool>(type: "boolean", nullable: false),
                    Echocardiogram = table.Column<bool>(type: "boolean", nullable: false),
                    StressTest = table.Column<bool>(type: "boolean", nullable: false),
                    HolterMonitor = table.Column<bool>(type: "boolean", nullable: false),
                    EventMonitor = table.Column<bool>(type: "boolean", nullable: false),
                    CardiacMRI = table.Column<bool>(type: "boolean", nullable: false),
                    CardiacCT = table.Column<bool>(type: "boolean", nullable: false),
                    CoronaryAngiogram = table.Column<bool>(type: "boolean", nullable: false),
                    CalciumScoreTest = table.Column<bool>(type: "boolean", nullable: false),
                    BloodPressureMonitoring = table.Column<bool>(type: "boolean", nullable: false),
                    RestingHeartRate = table.Column<int>(type: "integer", nullable: false),
                    MaxHeartRate = table.Column<int>(type: "integer", nullable: false),
                    SystolicBP = table.Column<double>(type: "double precision", nullable: false),
                    DiastolicBP = table.Column<double>(type: "double precision", nullable: false),
                    CholesterolLevel = table.Column<double>(type: "double precision", nullable: false),
                    TriglycerideLevel = table.Column<double>(type: "double precision", nullable: false),
                    LDLCholesterol = table.Column<double>(type: "double precision", nullable: false),
                    HDLCholesterol = table.Column<double>(type: "double precision", nullable: false),
                    EjectionFraction = table.Column<double>(type: "double precision", nullable: false),
                    CoronaryCalciumScore = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardiacTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardiacTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CardiacTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardiacTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EndocrineTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    ThyroidFunctionTest = table.Column<bool>(type: "boolean", nullable: false),
                    CortisolTest = table.Column<bool>(type: "boolean", nullable: false),
                    GrowthHormoneTest = table.Column<bool>(type: "boolean", nullable: false),
                    InsulinTest = table.Column<bool>(type: "boolean", nullable: false),
                    CPeptideTest = table.Column<bool>(type: "boolean", nullable: false),
                    ACTHStimulationTest = table.Column<bool>(type: "boolean", nullable: false),
                    DHEASulfateTest = table.Column<bool>(type: "boolean", nullable: false),
                    TestosteroneTest = table.Column<bool>(type: "boolean", nullable: false),
                    EstrogenTest = table.Column<bool>(type: "boolean", nullable: false),
                    ProgesteroneTest = table.Column<bool>(type: "boolean", nullable: false),
                    ParathyroidHormoneTest = table.Column<bool>(type: "boolean", nullable: false),
                    ProlactinTest = table.Column<bool>(type: "boolean", nullable: false),
                    TSH = table.Column<double>(type: "double precision", nullable: false),
                    FreeT3 = table.Column<double>(type: "double precision", nullable: false),
                    FreeT4 = table.Column<double>(type: "double precision", nullable: false),
                    CortisolLevel = table.Column<double>(type: "double precision", nullable: false),
                    InsulinLevel = table.Column<double>(type: "double precision", nullable: false),
                    BloodGlucose = table.Column<double>(type: "double precision", nullable: false),
                    HemoglobinA1c = table.Column<double>(type: "double precision", nullable: false),
                    DHEASulfate = table.Column<double>(type: "double precision", nullable: false),
                    Testosterone = table.Column<double>(type: "double precision", nullable: false),
                    Estrogen = table.Column<double>(type: "double precision", nullable: false),
                    Progesterone = table.Column<double>(type: "double precision", nullable: false),
                    Prolactin = table.Column<double>(type: "double precision", nullable: false),
                    ParathyroidHormone = table.Column<double>(type: "double precision", nullable: false),
                    CalciumLevel = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndocrineTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndocrineTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EndocrineTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EndocrineTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeneticTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CarrierScreening = table.Column<bool>(type: "boolean", nullable: false),
                    WholeExomeSequencing = table.Column<bool>(type: "boolean", nullable: false),
                    WholeGenomeSequencing = table.Column<bool>(type: "boolean", nullable: false),
                    PharmacogeneticTest = table.Column<bool>(type: "boolean", nullable: false),
                    CancerGeneticTest = table.Column<bool>(type: "boolean", nullable: false),
                    CardiovascularGeneticTest = table.Column<bool>(type: "boolean", nullable: false),
                    NeurologicalGeneticTest = table.Column<bool>(type: "boolean", nullable: false),
                    RareDiseaseTest = table.Column<bool>(type: "boolean", nullable: false),
                    BRCA1Mutation = table.Column<string>(type: "text", nullable: false),
                    BRCA2Mutation = table.Column<string>(type: "text", nullable: false),
                    MTHFRMutation = table.Column<string>(type: "text", nullable: false),
                    APCMutation = table.Column<string>(type: "text", nullable: false),
                    LRRK2Mutation = table.Column<string>(type: "text", nullable: false),
                    CFTRMutation = table.Column<string>(type: "text", nullable: false),
                    HBBMutation = table.Column<string>(type: "text", nullable: false),
                    HTTMutation = table.Column<string>(type: "text", nullable: false),
                    LDLRMutation = table.Column<string>(type: "text", nullable: false),
                    DiabetesRiskScore = table.Column<double>(type: "double precision", nullable: false),
                    HeartDiseaseRiskScore = table.Column<double>(type: "double precision", nullable: false),
                    AlzheimerRiskScore = table.Column<double>(type: "double precision", nullable: false),
                    ObesityRiskScore = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneticTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GeneticTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneticTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImagingReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    XRay = table.Column<bool>(type: "boolean", nullable: false),
                    CTScan = table.Column<bool>(type: "boolean", nullable: false),
                    MRI = table.Column<bool>(type: "boolean", nullable: false),
                    Ultrasound = table.Column<bool>(type: "boolean", nullable: false),
                    Mammogram = table.Column<bool>(type: "boolean", nullable: false),
                    PETScan = table.Column<bool>(type: "boolean", nullable: false),
                    BoneDensityScan = table.Column<bool>(type: "boolean", nullable: false),
                    Echocardiogram = table.Column<bool>(type: "boolean", nullable: false),
                    DopplerUltrasound = table.Column<bool>(type: "boolean", nullable: false),
                    GeneralFindings = table.Column<string>(type: "text", nullable: false),
                    Impression = table.Column<string>(type: "text", nullable: false),
                    RadiologistNotes = table.Column<string>(type: "text", nullable: false),
                    TumorSize = table.Column<double>(type: "double precision", nullable: false),
                    AneurysmDiameter = table.Column<double>(type: "double precision", nullable: false),
                    BoneDensity = table.Column<double>(type: "double precision", nullable: false),
                    EjectionFraction = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingReports_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImagingReports_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingReports_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InfectiousDiseaseTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Covid19 = table.Column<bool>(type: "boolean", nullable: false),
                    Influenza = table.Column<bool>(type: "boolean", nullable: false),
                    Tuberculosis = table.Column<bool>(type: "boolean", nullable: false),
                    HepatitisB = table.Column<bool>(type: "boolean", nullable: false),
                    HepatitisC = table.Column<bool>(type: "boolean", nullable: false),
                    HIV = table.Column<bool>(type: "boolean", nullable: false),
                    Syphilis = table.Column<bool>(type: "boolean", nullable: false),
                    Malaria = table.Column<bool>(type: "boolean", nullable: false),
                    Dengue = table.Column<bool>(type: "boolean", nullable: false),
                    LymeDisease = table.Column<bool>(type: "boolean", nullable: false),
                    ZikaVirus = table.Column<bool>(type: "boolean", nullable: false),
                    EpsteinBarrVirus = table.Column<bool>(type: "boolean", nullable: false),
                    Chlamydia = table.Column<bool>(type: "boolean", nullable: false),
                    Gonorrhea = table.Column<bool>(type: "boolean", nullable: false),
                    MRSA = table.Column<bool>(type: "boolean", nullable: false),
                    TestMethod = table.Column<string>(type: "text", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfectiousDiseaseTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfectiousDiseaseTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InfectiousDiseaseTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InfectiousDiseaseTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    TestLab = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Hemoglobin = table.Column<double>(type: "double precision", nullable: false),
                    WhiteBloodCellCount = table.Column<double>(type: "double precision", nullable: false),
                    PlateletCount = table.Column<double>(type: "double precision", nullable: false),
                    RedBloodCellCount = table.Column<double>(type: "double precision", nullable: false),
                    Glucose = table.Column<double>(type: "double precision", nullable: false),
                    Sodium = table.Column<double>(type: "double precision", nullable: false),
                    Potassium = table.Column<double>(type: "double precision", nullable: false),
                    Calcium = table.Column<double>(type: "double precision", nullable: false),
                    Cholesterol = table.Column<double>(type: "double precision", nullable: false),
                    ALT = table.Column<double>(type: "double precision", nullable: false),
                    AST = table.Column<double>(type: "double precision", nullable: false),
                    Bilirubin = table.Column<double>(type: "double precision", nullable: false),
                    Creatinine = table.Column<double>(type: "double precision", nullable: false),
                    BloodUreaNitrogen = table.Column<double>(type: "double precision", nullable: false),
                    GFR = table.Column<double>(type: "double precision", nullable: false),
                    UrineProtein = table.Column<bool>(type: "boolean", nullable: false),
                    UrineGlucose = table.Column<bool>(type: "boolean", nullable: false),
                    UrineKetones = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LaboratoryTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoryTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NeurologicalTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    BabinskiSign = table.Column<bool>(type: "boolean", nullable: false),
                    RombergTest = table.Column<bool>(type: "boolean", nullable: false),
                    FingerNoseTest = table.Column<bool>(type: "boolean", nullable: false),
                    GaitAssessment = table.Column<bool>(type: "boolean", nullable: false),
                    MuscleWeakness = table.Column<bool>(type: "boolean", nullable: false),
                    Tremors = table.Column<bool>(type: "boolean", nullable: false),
                    LightTouchTest = table.Column<bool>(type: "boolean", nullable: false),
                    PainSensationTest = table.Column<bool>(type: "boolean", nullable: false),
                    TemperatureSensationTest = table.Column<bool>(type: "boolean", nullable: false),
                    VibratorySensationTest = table.Column<bool>(type: "boolean", nullable: false),
                    PositionSenseTest = table.Column<bool>(type: "boolean", nullable: false),
                    MiniMentalStateExam = table.Column<bool>(type: "boolean", nullable: false),
                    MMSEScore = table.Column<int>(type: "integer", nullable: false),
                    ClockDrawingTest = table.Column<bool>(type: "boolean", nullable: false),
                    TrailMakingTest = table.Column<bool>(type: "boolean", nullable: false),
                    VerbalFluencyTest = table.Column<bool>(type: "boolean", nullable: false),
                    VisionAbnormality = table.Column<bool>(type: "boolean", nullable: false),
                    FacialNerveWeakness = table.Column<bool>(type: "boolean", nullable: false),
                    HearingLoss = table.Column<bool>(type: "boolean", nullable: false),
                    SwallowingDifficulty = table.Column<bool>(type: "boolean", nullable: false),
                    EEGPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    EEGResults = table.Column<string>(type: "text", nullable: false),
                    EMGPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    EMGResults = table.Column<string>(type: "text", nullable: false),
                    NCVPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    NCVResults = table.Column<string>(type: "text", nullable: false),
                    TestLab = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeurologicalTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeurologicalTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NeurologicalTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NeurologicalTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RespiratoryTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    SpirometryPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    FEV1 = table.Column<float>(type: "real", nullable: false),
                    FVC = table.Column<float>(type: "real", nullable: false),
                    FEV1FVC = table.Column<float>(type: "real", nullable: false),
                    PeakFlowTest = table.Column<bool>(type: "boolean", nullable: false),
                    PeakFlowRate = table.Column<float>(type: "real", nullable: false),
                    BloodGasTest = table.Column<bool>(type: "boolean", nullable: false),
                    OxygenSaturation = table.Column<float>(type: "real", nullable: false),
                    ArterialPaO2 = table.Column<float>(type: "real", nullable: false),
                    ArterialPaCO2 = table.Column<float>(type: "real", nullable: false),
                    BloodPH = table.Column<float>(type: "real", nullable: false),
                    ChestXRayPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    ChestXRayResults = table.Column<string>(type: "text", nullable: false),
                    CTScanPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    CTScanResults = table.Column<string>(type: "text", nullable: false),
                    BronchoscopyPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    BronchoscopyResults = table.Column<string>(type: "text", nullable: false),
                    TestLab = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespiratoryTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespiratoryTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespiratoryTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespiratoryTests_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedicalRecordId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    HeartRate = table.Column<float>(type: "real", nullable: false),
                    BloodPressureSystolic = table.Column<float>(type: "real", nullable: false),
                    BloodPressureDiastolic = table.Column<float>(type: "real", nullable: false),
                    RespiratoryRate = table.Column<float>(type: "real", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    OxygenSaturation = table.Column<float>(type: "real", nullable: false),
                    MeasurementLocation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalSigns_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VitalSigns_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VitalSigns_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TimeSlotId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true),
                    BillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergyTests_DoctorId",
                table: "AllergyTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyTests_MedicalRecordId",
                table: "AllergyTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyTests_MedicalRecordId1",
                table: "AllergyTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BillId",
                table: "Appointments",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TimeSlotId",
                table: "Appointments",
                column: "TimeSlotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_DoctorId",
                table: "BodyMeasurements",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_MedicalRecordId",
                table: "BodyMeasurements",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_MedicalRecordId1",
                table: "BodyMeasurements",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_CardiacTests_DoctorId",
                table: "CardiacTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CardiacTests_MedicalRecordId",
                table: "CardiacTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CardiacTests_MedicalRecordId1",
                table: "CardiacTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EndocrineTests_DoctorId",
                table: "EndocrineTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_EndocrineTests_MedicalRecordId",
                table: "EndocrineTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_EndocrineTests_MedicalRecordId1",
                table: "EndocrineTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTests_DoctorId",
                table: "GeneticTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTests_MedicalRecordId",
                table: "GeneticTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTests_MedicalRecordId1",
                table: "GeneticTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingReports_DoctorId",
                table: "ImagingReports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingReports_MedicalRecordId",
                table: "ImagingReports",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingReports_MedicalRecordId1",
                table: "ImagingReports",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_InfectiousDiseaseTests_DoctorId",
                table: "InfectiousDiseaseTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_InfectiousDiseaseTests_MedicalRecordId",
                table: "InfectiousDiseaseTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_InfectiousDiseaseTests_MedicalRecordId1",
                table: "InfectiousDiseaseTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryTests_DoctorId",
                table: "LaboratoryTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryTests_MedicalRecordId",
                table: "LaboratoryTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryTests_MedicalRecordId1",
                table: "LaboratoryTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalTests_DoctorId",
                table: "NeurologicalTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalTests_MedicalRecordId",
                table: "NeurologicalTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalTests_MedicalRecordId1",
                table: "NeurologicalTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_UserId",
                table: "Nurses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressId",
                table: "Patients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_BillId",
                table: "Prescriptions",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RespiratoryTests_DoctorId",
                table: "RespiratoryTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RespiratoryTests_MedicalRecordId",
                table: "RespiratoryTests",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RespiratoryTests_MedicalRecordId1",
                table: "RespiratoryTests",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId1",
                table: "Schedules",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ScheduleId",
                table: "TimeSlots",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_DoctorId",
                table: "VitalSigns",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_MedicalRecordId",
                table: "VitalSigns",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_MedicalRecordId1",
                table: "VitalSigns",
                column: "MedicalRecordId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergyTests");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BodyMeasurements");

            migrationBuilder.DropTable(
                name: "CardiacTests");

            migrationBuilder.DropTable(
                name: "EndocrineTests");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "GeneticTests");

            migrationBuilder.DropTable(
                name: "ImagingReports");

            migrationBuilder.DropTable(
                name: "InfectiousDiseaseTests");

            migrationBuilder.DropTable(
                name: "LaboratoryTests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "NeurologicalTests");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "RespiratoryTests");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
