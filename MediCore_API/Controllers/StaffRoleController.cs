using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffRoleController : ControllerBase
	{
		private readonly MediCoreContext context;

		public StaffRoleController(MediCoreContext context)
		{
			this.context = context;
		}

		
	}
}
