﻿using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using ESchool.Web.ViewModels.Question;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<SolvedQuiz> solvedQuizRepository;
        private readonly IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository;
        private readonly IQuizzesService quizzesService;

        public QuestionsService(
            IDeletableEntityRepository<Question> questionRepository,
            IDeletableEntityRepository<Quiz> quizRepository,
            IDeletableEntityRepository<Answer> answerRepository,
            IDeletableEntityRepository<SolvedQuiz> solvedQuizRepository,
            IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository,
            IQuizzesService quizzesService)
        {
            this.questionRepository = questionRepository;
            this.quizRepository = quizRepository;
            this.answerRepository = answerRepository;
            this.solvedQuizRepository = solvedQuizRepository;
            this.solvedQuestionRepository = solvedQuestionRepository;
            this.quizzesService = quizzesService;
        }

        public async Task CreateQuestionAsync(AddQuestionInputModel input)
        {
            var quiz = await this.quizRepository
                .AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    Questions = x.Questions.Count(),
                })
                .FirstOrDefaultAsync(x => x.Id == input.QuizId);

            var question = new Question
            {
                Number = quiz.Questions + 1,
                Text = input.Text,
                QuizId = input.QuizId,
            };

            foreach (var currentAnswer in input.Answers)
            {
                question.Answers.Add(
                    new Answer
                    {
                        Text = currentAnswer.Text,
                        IsRightAnswer = currentAnswer.IsRightAnswer,
                    });
            }

            await this.questionRepository.AddAsync(question);
            await this.questionRepository.SaveChangesAsync();
        }

        //public async Task DeleteQuestionByIdAsync(string id)
        //{
        //    var question = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //    this.repository.Delete(question);
        //    await this.repository.SaveChangesAsync();
        //    await this.UpdateAllQuestionsInQuizNumeration(question.QuizId);
        //}

        //public async Task UpdateAllQuestionsInQuizNumeration(string quizId)
        //{
        //    var count = 0;

        //    var questions = this.repository
        //      .AllAsNoTracking()
        //      .Where(x => x.QuizId == quizId)
        //      .OrderBy(x => x.Number);

        //    foreach (var question in questions)
        //    {
        //        question.Number = ++count;
        //        this.repository.Update(question);
        //    }

        //    await this.repository.SaveChangesAsync();
        //}

        //public async Task Update(string id, string text)
        //{
        //    var question = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //    question.Text = text;
        //    this.repository.Update(question);
        //    await this.repository.SaveChangesAsync();
        //}

        //public async Task<T> GetByIdAsync<T>(string id)
        //=> await this.repository.AllAsNoTracking()
        //    .Where(x => x.Id == id)
        //    .To<T>()
        //    .FirstOrDefaultAsync();

        //public async Task<IList<T>> GetAllByQuizIdAsync<T>(string id)
        //=> await this.repository.AllAsNoTracking()
        //    .Where(x => x.QuizId == id)
        //    .OrderBy(x => x.Number)
        //    .To<T>()
        //    .ToListAsync();

        //public async Task<int> GetAllByQuizIdCountAsync(string id)
        //=> await this.repository.AllAsNoTracking().Where(x => x.QuizId == id).CountAsync();

        public async Task<T> GetNextQuestion<T>(string quizId, string studentId)
        {
            var solvedQuiz = this.solvedQuizRepository
                .AllAsNoTracking()
                .Where(x => x.QuizId == quizId && x.StudentId == studentId)
                .FirstOrDefault();

            return await this.solvedQuestionRepository
                .AllAsNoTracking()
                .Where(x => x.SolvedQuizId == solvedQuiz.Id && x.StudentAnswer == null)
                .OrderBy(x => x.CreatedOn)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task AnswerQuestion(string solvedQuestionId, string answerText)
        {
            var solvedQuestion = this.solvedQuestionRepository
                .AllAsNoTracking()
                .Where(x => x.Id == solvedQuestionId)
                .FirstOrDefault();

            solvedQuestion.StudentAnswer = answerText;

            this.solvedQuestionRepository.Update(solvedQuestion);
            await this.solvedQuestionRepository.SaveChangesAsync();
        }
    }
}
