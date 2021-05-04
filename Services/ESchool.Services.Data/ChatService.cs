using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class ChatService : IChatService
    {
        private readonly IDeletableEntityRepository<Chat> chatRepository;

        public ChatService(
            IDeletableEntityRepository<Chat> chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public Task<Message> CreateMessage(string chatId, string message, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePrivateRoom(string rootId, string targetId)
        {
            throw new NotImplementedException();
        }

        public Task CreateRoom(string name, string userId)
        {
            throw new NotImplementedException();
        }

        public Chat GetChat(string id)
        {
            return this.chatRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Messages)
                .FirstOrDefault();
        }

        public IEnumerable<Chat> GetChats(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Chat> GetPrivateChats(string userId)
        {
            throw new NotImplementedException();
        }

        public Task JoinRoom(string chatId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
