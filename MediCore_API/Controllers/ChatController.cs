using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_API.Models.DTOs;
using MediCore_API.Models.DTOs.DTO_Entities;
using MediCore_API.Models.Entities;
using MediCore_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

		public ChatController(MediCoreContext context, UserManager<IdentityUser> userManager)
		{
			this.context = context;
			mapper = new ModelMapper();
			this.userManager = userManager;
		}

		[HttpGet("/All")]
		public async Task<ActionResult<List<ChatDTO>>> GetAllChats()
		{
			var chats = await context.Chats.Include(c => c.Messages).ToListAsync();
			if (!chats.Any()) return NotFound("No Messages Found");

			return Ok(chats.Select(c => mapper.Map<Chat, ChatDTO>(c)).ToList());
		}

		[HttpGet("/User/{id}")]
		public async Task<ActionResult<List<Chat>>> GetUserChats([FromRoute] string id)
		{
			var chats = await context.Chats.Where(c => c.Ids.Contains(id)).ToListAsync();
			return Ok(chats.Select(c => mapper.Map<Chat, ChatDTO>(c)).ToList());
		}

		[HttpGet("/User/DM/{id:Guid}")]
		public async Task<ActionResult<Chat>> GetChat([FromRoute] Guid id)
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

		[HttpPost("/User/{sendId}/Send-To/{recieveId}")]
		public async Task<ActionResult> SendMessage([FromRoute] string sendId, [FromRoute] string recieveId, [FromBody] MessageDTO message)
		{
			var chat = await context.Chats.Select(c => new Chat
			{
				Id = c.Id,
				Ids = c.Ids,
				Names = c.Names,
				Messages = c.Messages.OrderByDescending(m => m.Date).ToList()
			}).FirstOrDefaultAsync(c => c.Ids.Contains(sendId) && c.Ids.Contains(recieveId));

			Message newMessage = mapper.Map<MessageDTO, Message>(message);
			newMessage.SenderId = sendId;

			if (chat is null)
			{
				Chat newChat = new Chat
				{
					Ids = [sendId, recieveId],
					Names = [(await userManager.FindByIdAsync(sendId))!.UserName!, (await userManager.FindByIdAsync(recieveId))!.UserName!]
				};
				await context.Chats.AddAsync(newChat);
				await context.SaveChangesAsync();

				newMessage = new Message
				{
					Text = message.Text,
					SenderId = sendId,
					ChatId = newChat.Id
				};

				await context.Messages.AddAsync(newMessage);
				await context.SaveChangesAsync();

				return Created();
			}

			await context.Messages.AddAsync(newMessage);
			await context.SaveChangesAsync();

			return Created();
		}
	}
}
