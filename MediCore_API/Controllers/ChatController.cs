using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediCore_API.Data;
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
		private readonly IMapper mapper;
		private readonly UserManager<ApplicationUser> userManager;

		public ChatController(MediCoreContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult<List<ChatDTO>>> GetAllChats()
		{
			var chats = await context.Chats
				.Include(c => c.Messages)
				.ProjectTo<ChatDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(chats);
		}

		[HttpGet("user/{id:Guid}")]
		public async Task<ActionResult<List<ChatDTO>>> GetUserChats([FromRoute] Guid id)
		{
			var chats = await context.Chats
				.Where(c => c.Ids.Contains(id))
				.ProjectTo<ChatDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
			return Ok(chats);
		}

		[HttpGet("user/message/{id:Guid}")]
		public async Task<ActionResult<ChatDTO>> GetChat([FromRoute] Guid id)
		{
			var chat = await context.Chats
				.Select(c => new Chat
				{
					Id = c.Id,
					Ids = c.Ids,
					Names = c.Names,
					Messages = c.Messages.OrderByDescending(m => m.Date).ToList()
				})
				.ProjectTo<ChatDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(c => c.Id == id);
			if (chat is null) return NotFound("Chat Not Found");

			return Ok(chat);
		}

		[HttpPost("user/{sendId:Guid}/send-to/{receiveId:Guid}")]
		public async Task<ActionResult> SendMessage([FromRoute] Guid sendId, [FromRoute] Guid receiveId, [FromBody] MessageDTO message)
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

			Message newMessage = mapper.Map<Message>(message);
			newMessage.SenderId = sendId;
			newMessage.ChatId = chat.Id;

			await context.Messages.AddAsync(newMessage);
			await context.SaveChangesAsync();

			return Created();
		}

		[HttpDelete("{id:Guid}")]
		public async Task<ActionResult> DeleteChat([FromRoute] Guid id)
		{
			var chat = await context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
			if (chat is null) return NotFound("Chat Not Found");
			context.Messages.RemoveRange(chat.Messages);
			context.Chats.Remove(chat);
			await context.SaveChangesAsync();
			return Ok("Chat Deleted");
		}

		[HttpDelete("message/{id:Guid}")]
		public async Task<ActionResult> DeleteMessage([FromRoute] Guid id)
		{
			var message = await context.Messages.FirstOrDefaultAsync(m => m.Id == id);
			if (message is null) return NotFound("Message Not Found");
			context.Messages.Remove(message);
			await context.SaveChangesAsync();
			return Ok("Message Deleted");
		}
	}
}
