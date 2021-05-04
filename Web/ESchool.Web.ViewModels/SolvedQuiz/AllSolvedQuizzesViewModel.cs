namespace ESchool.Web.ViewModels.SolvedQuiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;

    public class AllSolvedQuizzesViewModel
    {
        public string QuizName { get; set; }

        public IEnumerable<SolvedQuizAtListViewModel> SolvedQuizzes { get; set; }
    }
}
