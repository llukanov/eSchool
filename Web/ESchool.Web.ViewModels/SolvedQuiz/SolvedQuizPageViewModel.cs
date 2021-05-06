namespace ESchool.Web.ViewModels.SolvedQuiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Question;
    using ESchool.Web.ViewModels.Quiz;

    public class SolvedQuizPageViewModel : IMapFrom<SolvedQuiz>
    {
        public string Id { get; set; }

        public QuizPageViewModel Quiz { get; set; }

        public IEnumerable<SolvedQuestionAtListViewModel> SolvedQuestions { get; set; }

        public int Scores { get; set; }

        public int TotalScores { get; set; }

        public string StudentId { get; set; }

        public string QuizId { get; set; }
    }
}
