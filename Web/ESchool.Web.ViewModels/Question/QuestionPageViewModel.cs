namespace ESchool.Web.ViewModels.Question
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class QuestionPageViewModel : IMapFrom<SolvedQuestion>
    {
        public string Id { get; set; }

        public string StudentAnswer { get; set; }

        public string QuizId { get; set; }

        public TakeQuestionViewModel Question { get; set; }
    }
}
