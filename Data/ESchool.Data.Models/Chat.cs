using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class Chat : BaseDeletableModel<string>
    {
        public Chat()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
            this.Users = new HashSet<ChatUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<ChatUser> Users { get; set; }
    }
}
