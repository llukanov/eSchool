using ESchool.Web.ViewModels.Quiz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IQuizzesService
    {
        Task<string> CreateQuizAsync(string name, string description, string creatorId, int lessonId);

        Task StartQuiz(string quizId, string studentId);

        //Task<IList<T>> GetAllUnAssignedToEventAsync<T>(string creatorId = null);

        //Task<IEnumerable<T>> GetAllPerPageAsync<T>(
        //    int page,
        //    int countPerPage,
        //    string creatorId = null,
        //    string searchCriteria = null,
        //    string searchText = null,
        //    string categoryId = null);

        //Task<IList<T>> GetAllByCategoryIdAsync<T>(string id);

        Task<T> GetQuizByIdAsync<T>(string id);

        T GetSolvedQuiz<T>(string quizId, string studentId);

        //Task<string> GetCreatorIdByQuizIdAsync(string id);

        //Task<string> GetQuizIdByPasswordAsync(string password);

        //Task<string> GetQuizNameByIdAsync(string id);

        //Task DeleteByIdAsync(string id);

        //Task DeleteEventFromQuizAsync(string eventId, string quizId);

        //Task UpdateAsync(string id, string name, string description, int? timer, string password);

        //Task<IList<T>> GetUnAssignedToCategoryByCreatorIdAsync<T>(string categoryId, string creatorId);

        //Task<bool[]> HasUserPermition(string userId, string quizId, bool isQuizTaken);

        //Task AssignQuizToEventAsync(string eventId, string quizId);

        //Task<int> GetAllQuizzesCountAsync(string creatorId = null, string searchCriteria = null, string searchText = null, string categoryId = null);

        IEnumerable<QuizAtListViewModel> GetAllQuizzesInLesson<T>(int lessonId);
    }
}
