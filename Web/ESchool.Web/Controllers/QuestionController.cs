namespace ESchool.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Question;
    using ESchool.Web.ViewModels.Quiz;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionController : Controller
    {
        private readonly IQuestionsService questionsService;
        private readonly IQuizzesService quizzesService;

        public QuestionController(
            IQuestionsService questionsService,
            IQuizzesService quizzesService)
        {
            this.questionsService = questionsService;
            this.quizzesService = quizzesService;
        }

        // Add Question to some quiz
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult AddQuestion(string quizId)
        {
            var viewModel = new AddQuestionInputModel();

            viewModel.Quiz =  this.quizzesService.GetQuizByIdAsync<QuizPageViewModel>(quizId);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> AddQuestion(string quizId, AddQuestionInputModel input)
        {
            input.Quiz = this.quizzesService.GetQuizByIdAsync<QuizPageViewModel>(quizId);
            

            try
            {
                await this.questionsService.CreateQuestionAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("AddQuestion", "Question", new { quizId = input.QuizId });
        }

        // Go on Next Question
        //[HttpGet]
        //[Authorize(Roles = GlobalConstants.StudentRoleName)]
        //public async Task<IActionResult> NextQuestion(string quizId)
        //{
        //    var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var quizModel = await this.questionsService.GetNextQuestion<QuestionPageViewModel>(quizId, currentUserId);

        //    quizModel.QuizId = quizId;

        //    return this.View(quizModel);
        //}

        //// Go on Next Question
        //[Authorize(Roles = GlobalConstants.StudentRoleName)]
        //public async Task<IActionResult> AnswerQuestion(string solvedQuestionId, string answerText, string quizId)
        //{
        //    try
        //    {
        //        await this.questionsService.AnswerQuestion(solvedQuestionId, answerText);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ModelState.AddModelError(string.Empty, ex.Message);
        //        return this.RedirectToAction("Index", "Home");
        //    }

        //    return this.RedirectToAction(nameof(this.NextQuestion), new { quizId = quizId });
        //}




        // Add Lesson in current subject from current teacher
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult LoadQuestion(string quizId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.questionsService.GetNextQuestion<QuestionPageViewModel>(quizId, currentUserId);

            if (viewModel == null)
            {
                return this.RedirectToAction(actionName: "FinishQuiz", controllerName: "Quiz", new { quizId = quizId });
            }

            viewModel.QuizId = quizId;

            return this.View(viewModel);
        }


        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> AnswerQuestion(string solvedQuestionId, string answerId, string quizId)
        {
            try
            {
                await this.questionsService.AnswerQuestion(solvedQuestionId, answerId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction(nameof(this.LoadQuestion), new { quizId = quizId });
        }
    }
}
