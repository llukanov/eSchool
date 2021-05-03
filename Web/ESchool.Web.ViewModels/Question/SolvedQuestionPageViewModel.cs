namespace ESchool.Web.ViewModels.Question
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SolvedQuestionPageViewModel : IMapFrom<SolvedQuestion>
    {
        public string Id { get; set; }

        public TakeQuestionViewModel Question { get; set; }

        public string StudentAnswer { get; set; }

        public int Scores { get; set; }
    }
}
