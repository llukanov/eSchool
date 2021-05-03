using ESchool.Common;
using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Question;
using ESchool.Web.ViewModels.Quiz;
using ESchool.Web.ViewModels.SolvedQuiz;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository;

        public SolvedQuizController(
            IQuizzesService quizzesService,
            IQuestionsService questionsService,
            IDeletableEntityRepository<SolvedQuestion> solvedQuestionRepository)
        {
            this.quizzesService = quizzesService;
            this.questionsService = questionsService;
            this.solvedQuestionRepository = solvedQuestionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ById(string quizId, string studentId)
        {
            var viewModel = this.quizzesService.GetSolvedQuiz<SolvedQuizPageViewModel>(quizId, studentId);

            viewModel.SolvedQuestions = this.questionsService.GetAllSolvedQuestionInQuiz<SolvedQuestionAtListViewModel>(viewModel.Id);

            viewModel.TotalScores = await this.quizzesService.GetQuizTotalScores(quizId);

            return this.View(viewModel);
        }
    }
}
