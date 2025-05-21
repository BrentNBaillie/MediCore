using MediCore_API.Data;
using MediCore_API.Interfaces;
using MediCore_Library.Models.DTOs.DTO_Entities;
using MediCore_Library.Models.Entities;
using MediCore_Library.Models.Identities;
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
		private readonly UserManager<ApplicationUser> userManager;

		public ChatController(MediCoreContext context, UserManager<ApplicationUser> userManager, IModelMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet]
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

		[HttpGet("user/{id:Guid}")]
		public async Task<ActionResult<List<ChatDTO>>> GetUserChats([FromRoute] Guid id)
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

		[HttpGet("user/message/{id:Guid}")]
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

		[HttpPost("user/{sendId:Guid}/send-to/{receiveId:Guid}")]
		public async Task<ActionResult> SendMessage([FromRoute] Guid sendId, [FromRoute] Guid receiveId, [FromBody] MessageDTO message)
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
						Names = [(await userManager.FindByIdAsync(sendId.ToString()))!.UserName!, (await userManager.FindByIdAsync(receiveId.ToString()))!.UserName!]
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

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteChat([FromRoute] Guid id)
		{
			try
			{
				var chat = await context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
				if (chat is null) return NotFound("Chat Not Found");
				context.Messages.RemoveRange(chat.Messages);
				context.Chats.Remove(chat);
				await context.SaveChangesAsync();
				return Ok("Chat Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}

		[HttpDelete("message/{id:Guid}")]
		public async Task<ActionResult> DeleteMessage([FromRoute] Guid id)
		{
			try
			{
				var message = await context.Messages.FirstOrDefaultAsync(m => m.Id == id);
				if (message is null) return NotFound("Message Not Found");
				context.Messages.Remove(message);
				await context.SaveChangesAsync();
				return Ok("Message Deleted");
			}
			catch (Exception e)
			{
				return StatusCode(500, $"Error: {e}");
			}
		}
	}
}
