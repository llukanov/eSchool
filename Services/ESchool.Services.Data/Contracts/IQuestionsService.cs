namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.Question;

    public interface IQuestionsService
    {
        Task CreateOneChoiceQuestionAsync(AddQuestionInputModel input);

        Task CreateTrueFalseQuestionAsync(AddTFQuestionInputModel input);

        Task CreateQuestionOpenAnswerAsync(AddQuestionOSInputModel input);

        T GetNextQuestion<T>(string quizId, string studentId);

        Task AnswerQuestion(string solvedQuestionId, string answerId);

        Task AnswerOpenQuestion(string solvedQuestionId, QuestionPageViewModel input);

        IEnumerable<SolvedQuestionAtListViewModel> GetAllSolvedQuestionInQuiz<T>(string solvedQuizId);
    }
}
