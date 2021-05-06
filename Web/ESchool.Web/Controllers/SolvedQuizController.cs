using ESchool.Common;
using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Question;
using ESchool.Web.ViewModels.Quiz;
using ESchool.Web.ViewModels.SolvedQuiz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Web.Controllers
{
    public class SolvedQuizController : Controller
    {
        private readonly IQuizzesService quizzesService;
        private readonly IQuestionsService questionsService;
        private readonly ISubjectsService subjectsService;
        private readonly IGradesService gradesService;
        private readonly IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository;

        public SolvedQuizController(
            IQuizzesService quizzesService,
            IQuestionsService questionsService,
            ISubjectsService subjectsService,
            IGradesService gradesService,
            IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository)
        {
            this.quizzesService = quizzesService;
            this.questionsService = questionsService;
            this.subjectsService = subjectsService;
            this.gradesService = gradesService;
            this.solvedQuestionRepository = solvedQuestionRepository;
        }

        // Get list with all solved quizzes by quizId
        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> AllSolvedQuizzes(string quizId)
        {
            var viewModel = new AllSolvedQuizzesViewModel
            {
                QuizName = await this.quizzesService.GetQuizNameByIdAsync(quizId),
                SubjectId = this.subjectsService.GetIdByQuizId(quizId),
                SolvedQuizzes = this.quizzesService.GetAllSolvedQuizzesByQuizId<SolvedQuizAtListViewModel>(quizId),
                Grades = this.gradesService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ById(string quizId, string studentId)
        {
            var viewModel = this.quizzesService.GetSolvedQuiz<SolvedQuizPageViewModel>(quizId, studentId);

            viewModel.SolvedQuestions = this.questionsService.GetAllSolvedQuestionInQuiz<SolvedQuestionAtListViewModel>(viewModel.Id);

            viewModel.TotalScores = await this.quizzesService.GetQuizTotalScores(quizId);

            viewModel.StudentId = studentId;

            viewModel.QuizId = quizId;

            return this.View(viewModel);
        }


        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Assign(string quizId, string studentId, string solvedQuestionId, int scores)
        {
           await this.questionsService.UpdateScores(solvedQuestionId, scores);

           return this.RedirectToAction(nameof(this.ById), new { quizId = quizId, studentId = studentId });
        }

        // Start a quiz by Id
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> FinalCheck(string solvedQuizId)
        {
            await this.quizzesService.FinishQuiz(solvedQuizId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
