using ESchool.Web.ViewModels.Question;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IQuestionsService
    {
        Task CreateOneChoiceQuestionAsync(AddQuestionInputModel input);

        Task CreateTrueFalseQuestionAsync(AddTFQuestionInputModel input);

        Task CreateQuestionOpenAnswerAsync(AddQuestionOSInputModel input);

        //Task DeleteQuestionByIdAsync(string id);

        //Task UpdateAllQuestionsInQuizNumeration(string quizId);

        //Task Update(string id, string text);

        //Task<T> GetByIdAsync<T>(string id);

        T GetNextQuestion<T>(string quizId, string studentId);

        Task AnswerQuestion(string solvedQuestionId, string answerId);

        //Task<IList<T>> GetAllByQuizIdAsync<T>(string id);

        //Task<int> GetAllByQuizIdCountAsync(string id);

        IEnumerable<SolvedQuestionAtListViewModel> GetAllSolvedQuestionInQuiz<T>(string solvedQuizId);
    }
}
