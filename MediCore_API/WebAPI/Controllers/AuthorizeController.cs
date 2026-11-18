using MediCore_API.Domain.Interfaces.IRepositories;
using MediCore_Library.Models.Identities;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizeController(IAuthorizeRep authRepo) : ControllerBase
	{
		//[Authorize(Roles = "admin")]
		[HttpPost("register/doctor")]
		public async Task<ActionResult> RegisterDoctor([FromBody] Register request)
		{
			var result = await authRepo.RegisterDoctor(request);
			return result.IsSuccess ? Created() : BadRequest(result.Error.Description);
		}

		//[Authorize(Roles = "admin, doctor, nurse")]
		[HttpPost("register/patient")]
		public async Task<ActionResult> RegisterPatient([FromBody] Register request)
		{
			var result = await authRepo.RegisterPatient(request);
			return result.IsSuccess ? Created() : BadRequest(result.Error.Description);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("register/nurse")]
		public async Task<ActionResult> RegisterNurse([FromBody] Register request)
		{
			var result = await authRepo.RegisterNurse(request);
			return result.IsSuccess ? Created() : BadRequest(result.Error.Description);
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login([FromBody] Login request)
		{
			var result = await authRepo.Login(request);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error.Description);
		}
    }
}
