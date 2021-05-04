namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public string ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
