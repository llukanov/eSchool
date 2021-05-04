namespace ESchool.Web.ViewModels.SolvedQuiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Question;
    using ESchool.Web.ViewModels.Student;

    public class SolvedQuizAtListViewModel : IMapFrom<SolvedQuiz>
    {
        public string Id { get; set; }

        public string QuizId { get; set; }

        public StudentAtListViewModel Student { get; set; }

        public IEnumerable<SolvedQuestionAtListViewModel> SolvedQuestions { get; set; }

        public int Scores { get; set; }
    }
}
