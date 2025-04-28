using MediCore_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		private readonly MediCoreContext context;

		public MessageController(MediCoreContext context)
		{
			this.context = context;
		}
	}
}
