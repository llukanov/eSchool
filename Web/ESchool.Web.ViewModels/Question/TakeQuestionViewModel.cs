namespace ESchool.Web.ViewModels.Question
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Answer;

    public class TakeQuestionViewModel : IMapFrom<Question>
    {
        public TakeQuestionViewModel()
        {
            this.Answers = new List<TakeQuizAnswerViewModel>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public IList<TakeQuizAnswerViewModel> Answers { get; set; }

        public int Scores { get; set; }

        public string QuestionType { get; set; }
    }
}
