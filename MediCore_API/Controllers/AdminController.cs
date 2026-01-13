using AutoMapper;
using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MediCoreContext context;
        private readonly IMapper mapper;
        private readonly IModelValidation validate;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(MediCoreContext context, IMapper mapper, IModelValidation validate, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.validate = validate;
        }

        [HttpGet("count")]
        public async Task<ActionResult<SystemCountsDTO>> GetAllCounts()
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

            return Ok(counts);
        }

        [HttpGet("salaries")]
        public async Task<ActionResult<TotalSalariesDTO>> GetSalarySums()
        {
            TotalSalariesDTO salaries = new TotalSalariesDTO
            {
                TotalDoctorSalaries = await context.Doctors.SumAsync(d => d.Salary),
                TotalNurseSalaries = await context.Nurses.SumAsync(n => n.Salary),
            };
            return Ok(salaries);
        }

        [HttpGet("user-count")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            int count = await userManager.Users.CountAsync();
            return Ok(count);
        }
    }
}
