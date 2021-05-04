using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class ChatUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
