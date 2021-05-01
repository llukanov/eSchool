namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class SolvedQuestion : BaseDeletableModel<string>
    {
        public SolvedQuestion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string StudentAnswer { get; set; }

        public string SolvedQuizId { get; set; }

        public virtual SolvedQuiz SolvedQuiz { get; set; }
    }
}
