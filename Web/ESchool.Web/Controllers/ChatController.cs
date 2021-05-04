using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESchool.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly IDeletableEntityRepository<Message> messageRepository;
        private readonly IRepository<ChatUser> chatUserRepository;
        private readonly IHubContext<ChatHub> hubContext;

        public ChatController(
            IChatService chatService,
            IDeletableEntityRepository<Message> messageRepository,
            IRepository<ChatUser> chatUserRepository,
            IHubContext<ChatHub> hubContext)
        {
            this.chatService = chatService;
            this.messageRepository = messageRepository;
            this.chatUserRepository = chatUserRepository;
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ById(string id)
        {
            return this.View(this.chatService.GetChat(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(
            string chatId,
            string message,
            string roomName)
        {
            var Message = new Message
            {
                ChatId = "6de6d859-85c4-4522-a8a1-2557e86b2f87",
                Text = message,
                Name = this.User.Identity.Name,
                Timestamp = DateTime.Now,
            };

            await this.messageRepository.AddAsync(Message);
            await this.messageRepository.SaveChangesAsync();

            await this.hubContext.Clients.Group(roomName)
                .SendAsync("ReceivedMessage", Message);

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
        {
            //var charUser = new ChatUser
            //{
            //    ChatId = id,
            //    UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            //};

            //await this.chatUserRepository.AddAsync(charUser);
            //await this.chatUserRepository.SaveChangesAsync();

            //return this.RedirectToAction("ById", new { id = "6de6d859-85c4-4522-a8a1-2557e86b2f87" });

            await this.hubContext.Groups.AddToGroupAsync(connectionId, roomName);

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            //var charUser = new ChatUser
            //{
            //    ChatId = id,
            //    UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            //};

            //await this.chatUserRepository.AddAsync(charUser);
            //await this.chatUserRepository.SaveChangesAsync();

            //return this.RedirectToAction("ById", new { id = "6de6d859-85c4-4522-a8a1-2557e86b2f87" });

            await this.hubContext.Groups.RemoveFromGroupAsync(connectionId, roomName);

            return this.Ok();
        }


    }
}
