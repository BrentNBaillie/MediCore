using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_API.Services;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediCore_Library.Models.ErrorHandle;
using Microsoft.EntityFrameworkCore;


namespace MediCore_API.Repositories
{
    public class AuthorizeRepo(MediCoreContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtTokenService jwtTokenService) : IAuthorizeRep
    {
        public async Task<Result<Doctor>> RegisterDoctor([FromBody] Register request)
        {
            if (request.Doctor is null) return Result<Doctor>.Failure(Error.BadRequest($"User data missing."));

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null) return Result<Doctor>.Failure(Error.BadRequest($"User with email: {request.Email} already exists."));

            var user = new ApplicationUser
            {
                UserName = $"{request.Doctor!.FirstName} {request.Doctor.LastName}",
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return Result<Doctor>.Failure(Error.Failure(result.Errors.Select(e => e.Description).FirstOrDefault() ?? string.Empty));

            await userManager.AddToRoleAsync(user, "doctor");

            var doctor = new Doctor
            {
                FirstName = request.Doctor.FirstName,
                LastName = request.Doctor.LastName,
                Specialization = request.Doctor.Specialization,
                PhoneNumber = request.Doctor.PhoneNumber,
                HospitalName = request.Doctor.HospitalName,
                ProfessionalBio = request.Doctor.ProfessionalBio,
                Salary = request.Doctor.Salary,
                UserId = user.Id
            };

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
            return Result<Doctor>.Success(null);
        }

        public async Task<Result<Patient>> RegisterPatient([FromBody] Register request)
        {
            if (request.Patient is null) return Result<Patient>.Failure(Error.BadRequest($"User data missing."));

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null) return Result<Patient>.Failure(Error.BadRequest($"User with email: {request.Email} already exists."));

            var user = new ApplicationUser
            {
                UserName = $"{request.Patient.FirstName} {request.Patient.LastName}",
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return Result<Patient>.Failure(Error.Failure(result.Errors.Select(e => e.Description).FirstOrDefault() ?? string.Empty));

            await userManager.AddToRoleAsync(user, "patient");

            Address address = new Address
            {
                Street = request.Patient.Address!.Street,
                City = request.Patient.Address.City,
                ProvinceOrState = request.Patient.Address.ProvinceOrState,
                Country = request.Patient.Address.Country,
                PostalCode = request.Patient.Address.PostalCode,
            };

            await context.Addresses.AddAsync(address);
            await context.SaveChangesAsync();

            Patient patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                DateOfBirth = request.Patient.DateOfBirth,
                PhoneNumber = request.Patient.PhoneNumber,
                Gender = request.Patient.Gender,
                AddressId = address.Id,
                UserId = user.Id
            };

            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            return Result<Patient>.Success(null);
        }

        public async Task<Result<Nurse>> RegisterNurse([FromBody] Register request)
        {
            if (request.Nurse is null) return Result<Nurse>.Failure(Error.BadRequest($"User data missing."));

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null) return Result<Nurse>.Failure(Error.BadRequest($"User with email: {request.Email} already exists."));

            var user = new ApplicationUser
            {
                UserName = $"{request.Nurse.FirstName} {request.Nurse.LastName}",
                Email = request.Email,
                EmailConfirmed = true

            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return Result<Nurse>.Failure(Error.Failure(result.Errors.Select(e => e.Description).FirstOrDefault() ?? string.Empty));

            await userManager.AddToRoleAsync(user, "nurse");

            var nurse = new Nurse
            {
                FirstName = request.Nurse.FirstName,
                LastName = request.Nurse.LastName,
                PhoneNumber = request.Nurse.PhoneNumber,
                Salary = request.Nurse.Salary,
                UserId = user.Id
            };

            await context.Nurses.AddAsync(nurse);
            await context.SaveChangesAsync();

            return Result<Nurse>.Success(null);
        }

        public async Task<Result<LoginResponse>> Login([FromBody] Login request)
        {
            Guid? id = Guid.Empty;
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null) return Result<LoginResponse>.Failure(Error.NotFound($"User with email: {request.Email} not found."));

            var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded) return Result<LoginResponse>.Failure(Error.Unauthorized($"Login failed. Incorrect email or password."));

            var token = await jwtTokenService.GenerateJwtTokenAsync(user);
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            if (role is null) return Result<LoginResponse>.Failure(Error.NotFound($"Role does not exist."));

            if (role == "doctor")
            {
                var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
                if (doctor is null) return Result<LoginResponse>.Failure(Error.NotFound($"Doctor doesnt exist."));
                id = doctor.Id;
            }
            if (role == "nurse")
            {
                var nurse = await context.Nurses.FirstOrDefaultAsync(d => d.UserId == user.Id);
                if (nurse is null) return Result<LoginResponse>.Failure(Error.NotFound($"Nurse does not exist."));
                id = nurse.Id;
            }
            if (role == "patient")
            {
                var patient = await context.Patients.FirstOrDefaultAsync(d => d.UserId == user.Id);
                if (patient is null) return Result<LoginResponse>.Failure(Error.NotFound($"Patient does not exist."));
                id = patient.Id;
            }

            await context.SaveChangesAsync();

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                Role = role,
                UserId = user.Id,
                ProfileId = id
            });
        }
    }
}
