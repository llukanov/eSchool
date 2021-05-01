namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public bool IsRightAnswer { get; set; }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
