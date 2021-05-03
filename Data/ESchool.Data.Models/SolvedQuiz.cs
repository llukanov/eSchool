namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class SolvedQuiz : BaseDeletableModel<string>
    {
        public SolvedQuiz()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SolvedQuestions = new HashSet<SolvedQuestion>();
        }

        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public virtual ICollection<SolvedQuestion> SolvedQuestions { get; set; }

        public int Scores { get; set; }
    }
}
