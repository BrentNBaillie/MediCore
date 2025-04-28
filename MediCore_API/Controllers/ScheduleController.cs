using MediCore_API.Models.DTOs;
using MediCore_API.Models.Entities;
using MediCore_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly MediCoreContext context;

        public ScheduleController(MediCoreContext context)
        {
            this.context = context;
        }


        
    }
}
