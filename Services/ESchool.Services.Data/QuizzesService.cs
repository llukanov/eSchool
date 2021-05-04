using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using ESchool.Web.ViewModels.Quiz;
using ESchool.Web.ViewModels.SolvedQuiz;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class QuizzesService : IQuizzesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IDeletableEntityRepository<SolvedQuiz> solvedQuizzesRepository;
        private readonly IDeletableEntityRepository<SolvedQuestion> solvedQuestionsRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;

        public QuizzesService(
            IDeletableEntityRepository<Quiz> quizRepository,
            IDeletableEntityRepository<SolvedQuiz> solvedQuizzesRepository,
            IDeletableEntityRepository<SolvedQuestion> solvedQuestionsRepository,
            IDeletableEntityRepository<Question> questionRepository)
        {
            this.quizRepository = quizRepository;
            this.solvedQuizzesRepository = solvedQuizzesRepository;
            this.solvedQuestionsRepository = solvedQuestionsRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<string> CreateQuizAsync(string name, string description, string creatorId, int lessonId)
        {
            var quiz = new Quiz
            {
                Name = name,
                Description = description,
                CreatorId = creatorId,
                LessonId = lessonId,
            };

            await this.quizRepository.AddAsync(quiz);
            await this.quizRepository.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task<bool> StartQuiz(string quizId, string studentId)
        {
            var solvedQuiz = this.solvedQuizzesRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == quizId && x.StudentId == studentId)
                .FirstOrDefault();

            // Check if quiz is solved by student
            if (solvedQuiz == null)
            {
                var quiz = this.quizRepository
                   .AllAsNoTracking()
                   .Where(x => x.Id == quizId)
                   .To<QuizPageViewModel>()
                   .FirstOrDefault();

                var newSolvedQuiz = new SolvedQuiz
                {
                    QuizId = quizId,
                    StudentId = studentId,
                };

                if (quiz.Questions.Any())
                {
                    foreach (var question in quiz.Questions)
                    {
                        newSolvedQuiz.SolvedQuestions.Add(
                            new SolvedQuestion
                            {
                                QuestionId = question.Id,
                            });
                    }
                }

                await this.solvedQuizzesRepository.AddAsync(newSolvedQuiz);
                await this.solvedQuizzesRepository.SaveChangesAsync();

                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task FinishQuiz(string quizId, string studentId)
        {
            var solvedQuiz = this.solvedQuizzesRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == quizId && x.StudentId == studentId)
                .FirstOrDefault();

            var solvedQuestions = this.solvedQuestionsRepository
                .AllAsNoTracking()
                .Where(x => x.SolvedQuizId == solvedQuiz.Id)
                .ToList();

            var quiz = this.quizRepository
                .AllAsNoTracking()
                .Where(x => x.Id == quizId)
                .To<QuizPageViewModel>()
                .FirstOrDefault();

            int totalStudentScores = 0;
            int totalScores = 0;

            if (solvedQuiz != null && solvedQuestions.Any())
            {
                foreach (var question in solvedQuestions)
                {
                    totalStudentScores += question.Scores;
                }

                solvedQuiz.Scores = totalStudentScores;

                this.solvedQuizzesRepository.Update(solvedQuiz);
                await this.solvedQuizzesRepository.SaveChangesAsync();
            }

            // Find total quiz's scores
            if (quiz != null)
            {
                foreach (var question in quiz.Questions)
                {
                    totalScores += question.Scores;
                }
            }
        }

        public T GetSolvedQuiz<T>(string quizId, string studentId)
        {
            var solvedQuiz = this.solvedQuizzesRepository.AllAsNoTracking()
                .Where(x => x.QuizId == quizId && x.StudentId == studentId)
                .To<T>()
                .FirstOrDefault();

            return solvedQuiz;
        }

        // public async Task<IList<T>> GetAllUnAssignedToEventAsync<T>(string creatorId = null)
        // {
        //     var query = this.quizRepository
        //            .AllAsNoTracking()
        //            .Where(x => x.EventId == null);

        //     if (creatorId != null)
        //     {
        //         query = query.Where(x => x.CreatorId == creatorId);
        //     }

        //     return await query.OrderByDescending(x => x.CreatedOn)
        //         .To<T>()
        //         .ToListAsync();
        // }

        public T GetQuizByIdAsync<T>(string id)
       => this.quizRepository
               .AllAsNoTracking()
               .Where(x => x.Id == id)
               .To<T>()
               .FirstOrDefault();

        public T GetSolvedQuizByIdAsync<T>(string id)
        {
            return this.solvedQuizzesRepository
                          .AllAsNoTracking()
                          .Where(x => x.Id == id)
                          .To<T>()
                          .FirstOrDefault();
        }


        // public async Task DeleteByIdAsync(string id)
        // {
        //     var quiz = await this.quizRepository
        //         .AllAsNoTracking()
        //         .FirstOrDefaultAsync(x => x.Id == id);
        //     var password = await this.passwordRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.QuizId == id)
        //         .FirstOrDefaultAsync();
        //     this.passwordRepository.Delete(password);
        //     this.quizRepository.Delete(quiz);

        //     await this.passwordRepository.SaveChangesAsync();
        //     await this.quizRepository.SaveChangesAsync();
        // }

        // public async Task UpdateAsync(string id, string name, string description, int? timer, string password)
        // {
        //     var quiz = await this.quizRepository
        //        .AllAsNoTracking()
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //     var passwordEntity = await this.passwordRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.QuizId == id)
        //         .FirstOrDefaultAsync();

        //     if (passwordEntity.Content != password)
        //     {
        //         passwordEntity.Content = password;
        //         this.passwordRepository.Update(passwordEntity);
        //         await this.passwordRepository.SaveChangesAsync();
        //     }

        //     quiz.Name = name;
        //     quiz.Description = description;
        //     quiz.Timer = timer;

        //     this.quizRepository.Update(quiz);
        //     await this.quizRepository.SaveChangesAsync();
        // }

        // public async Task<string> GetQuizIdByPasswordAsync(string password)
        // => await this.quizRepository.AllAsNoTracking()
        //     .Where(x => x.Password.Content == password)
        //     .Select(x => x.Id)
        //     .FirstOrDefaultAsync();

        // public async Task<bool[]> HasUserPermition(string userId, string quizId, bool isQuizTaken)
        // {
        //     var quizQuery = this.quizRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.Id == quizId);

        //     var creatorId = await quizQuery.Select(x => x.CreatorId).FirstOrDefaultAsync();
        //     if (creatorId == userId)
        //     {
        //         return new bool[] { true, true };
        //     }

        //     if (isQuizTaken)
        //     {
        //         return new bool[] { false, false };
        //     }

        //     var eventIsActive = await quizQuery.Select(x => x.Event.Status == Status.Active).FirstOrDefaultAsync();
        //     if (!eventIsActive)
        //     {
        //         return new bool[] { false, false };
        //     }

        //     var results = await quizQuery
        //         .SelectMany(x => x.Event.Results.Where(x => x.StudentId == userId))
        //         .ToListAsync();
        //     if (results.Count() > 0)
        //     {
        //         return new bool[] { false, false };
        //     }

        //     var eventGroups = await quizQuery
        //         .SelectMany(x => x.Event.EventsGroups
        //         .Where(x => x.Group.StudentstGroups
        //         .Any(x => x.StudentId == userId)))
        //         .ToListAsync();

        //     return eventGroups.Count() > 0 ? new bool[] { true, false } : new bool[] { false, false };
        // }

        // public async Task AssignQuizToEventAsync(string eventId, string quizId)
        // {
        //     var quiz = await this.quizRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.Id == quizId)
        //         .FirstOrDefaultAsync();

        //     quiz.EventId = eventId;
        //     this.quizRepository.Update(quiz);
        //     await this.quizRepository.SaveChangesAsync();
        // }

        // public async Task DeleteEventFromQuizAsync(string eventId, string quizId)
        // {
        //     var quiz = await this.quizRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.Id == quizId)
        //         .FirstOrDefaultAsync();

        //     quiz.EventId = null;
        //     this.quizRepository.Update(quiz);
        //     await this.quizRepository.SaveChangesAsync();
        // }

        // public async Task<IList<T>> GetUnAssignedToCategoryByCreatorIdAsync<T>(string categoryId, string creatorId)
        // => await this.quizRepository
        //     .AllAsNoTracking()
        //     .Where(x => x.CreatorId == creatorId && x.CategoryId != categoryId)
        //     .To<T>()
        //     .ToListAsync();

        // public async Task<IList<T>> GetAllByCategoryIdAsync<T>(string id)
        // => await this.quizRepository
        //     .AllAsNoTracking()
        //     .Where(x => x.CategoryId == id)
        //     .To<T>()
        //     .ToListAsync();

        // public async Task<string> GetCreatorIdByQuizIdAsync(string id)
        // => await this.quizRepository
        //         .AllAsNoTracking()
        //         .Where(x => x.Id == id)
        //         .Select(x => x.CreatorId)
        //         .FirstOrDefaultAsync();

        // public async Task<IEnumerable<T>> GetAllPerPageAsync<T>(
        //     int page,
        //     int countPerPage,
        //     string creatorId = null,
        //     string searchCriteria = null,
        //     string searchText = null,
        //     string categoryId = null)
        // {
        //     var query = this.quizRepository.AllAsNoTracking();

        //     if (creatorId != null)
        //     {
        //         query = query.Where(x => x.CreatorId == creatorId);
        //     }

        //     if (categoryId != null)
        //     {
        //         query = query.Where(x => x.CategoryId == categoryId);
        //     }

        //     var emptyNameInput = searchText == null && searchCriteria == ServicesConstants.Name;
        //     if (searchCriteria != null && !emptyNameInput)
        //     {
        //         var filter = this.expressionBuilder.GetExpression<Quiz>(searchCriteria, searchText);
        //         query = query.Where(filter);
        //     }

        //     return await query.OrderByDescending(x => x.CreatedOn)
        //     .Skip(countPerPage * (page - 1))
        //     .Take(countPerPage)
        //     .To<T>()
        //     .ToListAsync();
        // }

        // public async Task<int> GetAllQuizzesCountAsync(string creatorId = null, string searchCriteria = null, string searchText = null, string categoryId = null)
        // {
        //     var query = this.quizRepository.AllAsNoTracking();

        //     if (creatorId != null)
        //     {
        //         query = query.Where(x => x.CreatorId == creatorId);
        //     }

        //     if (categoryId != null)
        //     {
        //         query = query.Where(x => x.CategoryId == categoryId);
        //     }

        //     var emptyNameInput = searchText == null && searchCriteria == ServicesConstants.Name;
        //     if (searchCriteria != null && !emptyNameInput)
        //     {
        //         var filter = this.expressionBuilder.GetExpression<Quiz>(searchCriteria, searchText);
        //         query = query.Where(filter);
        //     }

        //     return await query.CountAsync();
        // }

        // public async Task<string> GetQuizNameByIdAsync(string id)
        // => await this.quizRepository.AllAsNoTracking()
        //      .Where(x => x.Id == id)
        //      .Select(x => x.Name)
        //      .FirstOrDefaultAsync();


        // Get all assignments in lesson by lessonId
        public IEnumerable<QuizAtListViewModel> GetAllQuizzesInLesson<T>(int lessonId)
        {
            var quizzes = this.quizRepository
                .AllAsNoTracking()
                .Where(x => x.LessonId == lessonId)
                .OrderBy(x => x.CreatedOn)
                .To<QuizAtListViewModel>()
                .ToList();

            return quizzes;
        }

        public async Task<int> GetQuizTotalScores(string quizId)
        {
            int totalScores = 0;

            var questions = await this.questionRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == quizId)
                .ToListAsync();

            foreach (var question in questions)
            {
                totalScores += question.Scores;
            }

            return totalScores;
        }

        public async Task ActivateQuiz(string quizId)
        {
            var quiz = this.quizRepository
               .AllAsNoTracking()
               .Where(x => x.Id == quizId)
               .FirstOrDefault();

            if (quiz != null)
            {
                quiz.IsActivated = true;

                this.quizRepository.Update(quiz);
                await this.quizRepository.SaveChangesAsync();
            }
        }
    }
}
