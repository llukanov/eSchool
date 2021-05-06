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

        public async Task FinishQuiz(string solvedQuizId)
        {
            var solvedQuiz = this.solvedQuizzesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == solvedQuizId)
                .FirstOrDefault();

            var solvedQuestions = this.solvedQuestionsRepository
                .AllAsNoTracking()
                .Where(x => x.SolvedQuizId == solvedQuiz.Id)
                .ToList();

            var quiz = this.quizRepository
                .AllAsNoTracking()
                .Where(x => x.Id == solvedQuiz.QuizId)
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

        public async Task<string> GetQuizNameByIdAsync(string quizId)
        {
            return await this.quizRepository.AllAsNoTracking()
                        .Where(x => x.Id == quizId)
                        .Select(x => x.Name)
                        .FirstOrDefaultAsync();
        }

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

        // Get all solved quizzes in quiz by its id
        public IEnumerable<SolvedQuizAtListViewModel> GetAllSolvedQuizzesByQuizId<T>(string quizId)
        {
            var solvedQuizzes = this.solvedQuizzesRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == quizId)
                .OrderBy(x => x.CreatedOn)
                .To<SolvedQuizAtListViewModel>()
                .ToList();

            return solvedQuizzes;
        }
    }
}
