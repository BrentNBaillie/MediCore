using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using MediCore_API.Data;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly MediCoreContext context;

		public DoctorController(MediCoreContext context)
		{
			this.context = context;
		}

		
    }
}
