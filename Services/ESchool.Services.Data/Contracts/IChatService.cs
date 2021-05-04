using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IChatService
    {
        Chat GetChat(string id);

        Task CreateRoom(string name, string userId);

        Task JoinRoom(string chatId, string userId);

        IEnumerable<Chat> GetChats(string userId);

        Task<int> CreatePrivateRoom(string rootId, string targetId);

        IEnumerable<Chat> GetPrivateChats(string userId);

        Task<Message> CreateMessage(string chatId, string message, string userId);
    }
}
