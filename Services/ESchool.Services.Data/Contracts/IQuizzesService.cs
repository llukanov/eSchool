namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.Quiz;
    using ESchool.Web.ViewModels.SolvedQuiz;

    public interface IQuizzesService
    {
        Task<string> CreateQuizAsync(string name, string description, string creatorId, int lessonId);

        Task<bool> StartQuiz(string quizId, string studentId);

        Task FinishQuiz(string solvedQuizId);

        Task ActivateQuiz(string quizId);

        T GetQuizByIdAsync<T>(string id);

        T GetSolvedQuiz<T>(string quizId, string studentId);

        T GetSolvedQuizByIdAsync<T>(string id);

        Task<string> GetQuizNameByIdAsync(string quizId);

        IEnumerable<QuizAtListViewModel> GetAllQuizzesInLesson<T>(int lessonId);

        IEnumerable<SolvedQuizAtListViewModel> GetAllSolvedQuizzesByQuizId<T>(string quizId);

        Task<int> GetQuizTotalScores(string quizId);
    }
}
