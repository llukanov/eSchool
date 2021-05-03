namespace ESchool.Web.ViewModels.Quiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Question;

    public class TakeQuizViewModel : IMapFrom<Quiz>
    {
        public TakeQuizViewModel()
        {
            this.Questions = new List<TakeQuestionViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<TakeQuestionViewModel> Questions { get; set; }
    }
}
