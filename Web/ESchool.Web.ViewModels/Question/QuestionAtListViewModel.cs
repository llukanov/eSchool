namespace ESchool.Web.ViewModels.Question
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Answer;

    public class QuestionAtListViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public IEnumerable<AnswerAtListViewModel> Answers { get; set; }

        public int Scores { get; set; }
    }
}
