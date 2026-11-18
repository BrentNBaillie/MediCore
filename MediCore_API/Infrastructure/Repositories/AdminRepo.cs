using AutoMapper;
using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_API.Domain.Interfaces.Iservices;
using MediCore_API.Infrastructure.Data;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.ErrorHandle;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Infrastructure.Repositories
{
    public class AdminRepo(MediCoreContext context, IMapper mapper, IModelValidation validate, UserManager<ApplicationUser> userManager) : IAdminRepo
    {
        public async Task<Result<SystemCountsDTO>> GetAllCounts()
        {
            SystemCountsDTO counts = new SystemCountsDTO
            {
                Addresses = await context.Addresses.CountAsync(),
                Appointments = await context.Appointments.CountAsync(),
                Users = await userManager.Users.CountAsync(),
                Bills = await context.Bills.CountAsync(),
                Chats = await context.Chats.CountAsync(),
                Doctors = await context.Doctors.CountAsync(),
                Feedbacks = await context.Feedbacks.CountAsync(),
                MedicalRecords = await context.MedicalRecords.CountAsync(),
                Medicines = await context.Medicines.CountAsync(),
                Nurses = await context.Nurses.CountAsync(),
                Patients = await context.Patients.CountAsync(),
                Prescriptions = await context.Prescriptions.CountAsync(),
                Schedules = await context.Schedules.CountAsync()
            };

            return Result<SystemCountsDTO>.Success(counts);
        }

        public async Task<Result<TotalSalariesDTO>> GetSalarySums()
        {
            TotalSalariesDTO salaries = new TotalSalariesDTO
            {
                TotalDoctorSalaries = await context.Doctors.SumAsync(d => d.Salary),
                TotalNurseSalaries = await context.Nurses.SumAsync(n => n.Salary),
            };
            return Result<TotalSalariesDTO>.Success(salaries);
        }

        public async Task<Result<int>> GetUserCount()
        {
            int count = await userManager.Users.CountAsync();
            return Result<int>.Success(count);
        }
    }
}
