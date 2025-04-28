using MediCore_API.Models.DTOs;
using MediCore_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicalRecordController : ControllerBase
	{
		private readonly MediCoreContext context;

		public MedicalRecordController(MediCoreContext context)
		{
			this.context = context;
		}
	}
}
