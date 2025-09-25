using MediCore_API.Interfaces;
using MediCore_API.Services;
using MediCore_API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_Library.Models.Identities;
using MediCore_Library.Models.Entities;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizeController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IJwtTokenService jwtTokenService;
		private readonly MediCoreContext context;

		public AuthorizeController(MediCoreContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			jwtTokenService = new JwtTokenService(config, userManager);
			this.context = context;
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("register/doctor")]
		public async Task<ActionResult> RegisterDoctor([FromBody] Register request)
		{
			if (request.Doctor is null) return BadRequest("Invalid Doctor Data");

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null) return BadRequest("Email already registered!");

			var user = new ApplicationUser
			{
				UserName = $"{request.Doctor.FirstName} {request.Doctor.LastName}",
				Email = request.Email,
				EmailConfirmed = true
			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return BadRequest(result.Errors.Select(e => e.Description));

			await userManager.AddToRoleAsync(user, "doctor");

			var doctor = new Doctor
			{
				FirstName = request.Doctor.FirstName,
				LastName = request.Doctor.LastName,
				Specialization = request.Doctor.Specialization,
				PhoneNumber = request.Doctor.PhoneNumber,
				HospitalName = request.Doctor.HospitalName,
				ProfessionalBio = request.Doctor.ProfessionalBio,
				UserId = user.Id
			};

			await context.Doctors.AddAsync(doctor);
			await context.SaveChangesAsync();

			return Created();
		}

		//[Authorize(Roles = "admin, doctor, nurse")]
		[HttpPost("register/patient")]
		public async Task<ActionResult> RegisterPatient([FromBody] Register request)
		{
			if (request.Patient is null) return BadRequest("Patient is null");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null) return BadRequest("Email already registered!");

			var user = new ApplicationUser
			{
				UserName = $"{request.Patient.FirstName} {request.Patient.LastName}",
				Email = request.Email,
				EmailConfirmed = true
			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
			{
				return BadRequest("Creation Failed");
			}

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

			return Created();
		}


		//[Authorize(Roles = "admin")]
		[HttpPost("register/nurse")]
		public async Task<ActionResult> RegisterNurse([FromBody] Register request)
		{
			if (request.Nurse is null) return BadRequest();

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null) return BadRequest();

			var user = new ApplicationUser
			{
				UserName = $"{request.Nurse.FirstName} {request.Nurse.LastName}",
				Email = request.Email,
				EmailConfirmed = true

			};

			var result = await userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return BadRequest();

			await userManager.AddToRoleAsync(user, "nurse");

			var nurse = new Nurse
			{
				FirstName = request.Nurse.FirstName,
				LastName = request.Nurse.LastName,
				PhoneNumber = request.Nurse.PhoneNumber,
				UserId = user.Id
			};

			await context.Nurses.AddAsync(nurse);
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login([FromBody] Login request)
		{
			Guid? id = Guid.Empty;
			var user = await userManager.FindByEmailAsync(request.Email);
			if (user is null) return NotFound("User Not Found");

			var result = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
			if (!result.Succeeded) return Unauthorized();

			var token = await jwtTokenService.GenerateJwtTokenAsync(user);
			var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
			if (role is null) return NotFound();

			if (role == "doctor")
			{
				var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
				if (doctor is null) return NotFound("Doctor does not exist");
				id = doctor.Id;
			}
			if (role == "nurse")
			{
				var nurse = await context.Nurses.FirstOrDefaultAsync(d => d.UserId == user.Id);
				if (nurse is null) return NotFound("Nurse does not exist.");
				id = nurse.Id;
			}
			if (role == "patient")
			{
				var patient = await context.Patients.FirstOrDefaultAsync(d => d.UserId == user.Id);
				if (patient is null) return NotFound("Patient does not exist.");
				id = patient.Id;
			}

			await context.SaveChangesAsync();

			return Ok(new LoginResponse
			{
				Token = token,
				Role = role,
				UserId = user.Id,
				ProfileId = id
			});
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteAllUsers()
		{
			var users = await userManager.Users.ToListAsync();

			foreach (var user in users)
			{
				var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
				if (role == "doctor")
				{
					var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (doctor is not null)
					{
						context.Doctors.Remove(doctor);
						await context.SaveChangesAsync();
					}
				}
				if (role == "patient")
				{
					var patient = await context.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);
					if (patient is not null)
					{
						context.Patients.Remove(patient);
						await context.SaveChangesAsync();
					}
				}
				if (role == "nurse")
				{
					var nurse = await context.Nurses.FirstOrDefaultAsync(s => s.UserId == user.Id);
					if (nurse is not null)
					{
						context.Nurses.Remove(nurse);
						await context.SaveChangesAsync();
					}
				}
			}

			return Ok($"{users.Count - 1} Users Deleted");
		}
    }
}
