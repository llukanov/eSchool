namespace ESchool.Web.ViewModels.SolvedQuiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;

    public class AllSolvedQuizzesViewModel
    {
        public string QuizName { get; set; }

        public int SubjectId { get; set; }

        public IEnumerable<SolvedQuizAtListViewModel> SolvedQuizzes { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Grades { get; set; }
    }
}
