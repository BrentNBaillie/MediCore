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
			try
			{
				if (request.Doctor is null) return BadRequest("Invalid Doctor Data");

				var existingUser = await userManager.FindByEmailAsync(request.Email);
				if (existingUser != null) return BadRequest("Email already registered!");

				var user = new ApplicationUser
				{
					UserName = request.UserName,
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
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		//[Authorize(Roles = "admin, doctor, staff")]
		[HttpPost("register/patient")]
		public async Task<ActionResult> RegisterPatient([FromBody] Register request)
		{
			try
			{
				if (request.Patient is null) return BadRequest();

				if (!ModelState.IsValid) return BadRequest(ModelState);

				var existingUser = await userManager.FindByEmailAsync(request.Email);
				if (existingUser != null) return BadRequest("Email already registered!");

				var user = new ApplicationUser
				{
					UserName = request.UserName,
					Email = request.Email,
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(user, request.Password);
				if (!result.Succeeded)
				{
					return BadRequest(new { message = "User creation failed", errors = result.Errors });
				}

				await userManager.AddToRoleAsync(user, "patient");

				Patient patient = new Patient
				{
					FirstName = request.Patient.FirstName,
					LastName = request.Patient.LastName,
					DateOfBirth = request.Patient.DateOfBirth,
					PhoneNumber = request.Patient.PhoneNumber,
					Gender = request.Patient.Gender,
					UserId = user.Id
				};

				await context.Patients.AddAsync(patient);
				await context.SaveChangesAsync();

				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}


		//[Authorize(Roles = "admin")]
		[HttpPost("register/staff")]
		public async Task<ActionResult> RegisterStaff([FromBody] Register request)
		{
			try
			{
				if (request.Staff is null) return BadRequest();

				var existingUser = await userManager.FindByEmailAsync(request.Email);
				if (existingUser != null) return BadRequest();

				var user = new ApplicationUser
				{
					UserName = request.UserName,
					Email = request.Email,
					EmailConfirmed = true

				};

				var result = await userManager.CreateAsync(user, request.Password);
				if (!result.Succeeded) return BadRequest();

				await userManager.AddToRoleAsync(user, "staff");

				var staff = new Staff
				{
					FirstName = request.Staff.FirstName,
					LastName = request.Staff.LastName,
					PhoneNumber = request.Staff.PhoneNumber,
					RoleId = request.Staff.RoleId,
					UserId = user.Id
				};

				await context.StaffMembers.AddAsync(staff);
				await context.SaveChangesAsync();

				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login([FromBody] Login request)
		{
			try
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
				if (role == "staff")
				{
					var staff = await context.StaffMembers.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (staff is null) return NotFound("Staff member does not exist.");
					id = staff.Id;
				}
				if (role == "patient")
				{
					var patient = await context.Patients.FirstOrDefaultAsync(d => d.UserId == user.Id);
					if (patient is null) return NotFound("Patient does not exist.");
					id = patient.Id;
				}

				return Ok(new LoginResponse
				{
					Token = token,
					Role = role,
					Id = id
				});
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteAllUsers()
		{
			try
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
					if (role == "staff")
					{
						var staff = await context.StaffMembers.FirstOrDefaultAsync(s => s.UserId == user.Id);
						if (staff is not null)
						{
							context.StaffMembers.Remove(staff);
							await context.SaveChangesAsync();
						}
					}
					if (user is not null)
					{
						await userManager.DeleteAsync(user);
					}
				}

				return Ok("All Users Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
    }
}
