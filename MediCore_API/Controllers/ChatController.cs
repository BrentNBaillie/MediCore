using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediCore_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly MediCoreContext context;
		private readonly IModelMapper mapper;
		private readonly UserManager<IdentityUser> userManager;

		public ChatController(MediCoreContext context, UserManager<IdentityUser> userManager, IModelMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet("All")]
		public async Task<ActionResult<List<ChatDTO>>> GetAllChats()
		{
			try
			{
				var chats = await context.Chats.Include(c => c.Messages).ToListAsync();
				return Ok(chats.Select(c => mapper.Map<Chat, ChatDTO>(c)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("User/{id}")]
		public async Task<ActionResult<List<ChatDTO>>> GetUserChats([FromRoute] string id)
		{
			try
			{
				var chats = await context.Chats.Where(c => c.Ids.Contains(id)).ToListAsync();
				return Ok(chats.Select(c => mapper.Map<Chat, ChatDTO>(c)).ToList());
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpGet("User/DM/{id:Guid}")]
		public async Task<ActionResult<ChatDTO>> GetChat([FromRoute] Guid id)
		{
			try
			{
				var chat = await context.Chats.Select(c => new Chat
				{
					Id = c.Id,
					Ids = c.Ids,
					Names = c.Names,
					Messages = c.Messages.OrderByDescending(m => m.Date).ToList()
				}).FirstOrDefaultAsync(c => c.Id == id);
				if (chat is null) return NotFound("Chat Not Found");

				return Ok(mapper.Map<Chat, ChatDTO>(chat));
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpPost("User/{sendId}/Send-To/{receiveId}")]
		public async Task<ActionResult> SendMessage([FromRoute] string sendId, [FromRoute] string receiveId, [FromBody] MessageDTO message)
		{
			try
			{
				var chat = await context.Chats.Select(c => new Chat
				{
					Id = c.Id,
					Ids = c.Ids,
					Names = c.Names,
					Messages = c.Messages.OrderByDescending(m => m.Date).ToList()
				}).FirstOrDefaultAsync(c => c.Ids.Contains(sendId) && c.Ids.Contains(receiveId));

				if (chat is null)
				{
					chat = new Chat
					{
						Ids = [sendId, receiveId],
						Names = [(await userManager.FindByIdAsync(sendId))!.UserName!, (await userManager.FindByIdAsync(receiveId))!.UserName!]
					};
					await context.Chats.AddAsync(chat);
					await context.SaveChangesAsync();
				}

				Message newMessage = mapper.Map<MessageDTO, Message>(message);
				newMessage.SenderId = sendId;
				newMessage.ChatId = chat.Id;

				await context.Messages.AddAsync(newMessage);
				await context.SaveChangesAsync();

				return Created();
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
