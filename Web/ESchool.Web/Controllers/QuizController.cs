namespace ESchool.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Quiz;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class QuizController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IQuizzesService quizzesService;

        public QuizController(
            UserManager<ApplicationUser> userManager,
            IQuizzesService quizzesService)
        {
            this.userManager = userManager;
            this.quizzesService = quizzesService;
        }

        // Create a quiz in some Lesson
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Create(int lessonId)
        {
            var viewModel = new CreateQuizInputModel();

            viewModel.LessonId = lessonId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(CreateQuizInputModel viewModel)
        {
            string quizId;

            try
            {
                var currentUser = await this.userManager.GetUserAsync(this.User);

                viewModel.CreatorId = currentUser.Id;

                quizId = await this.quizzesService.CreateQuizAsync(viewModel.Name, viewModel.Description, viewModel.CreatorId, viewModel.LessonId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(viewModel);
            }

            return this.RedirectToAction("AddQuestion", "Question", new { quizId = quizId });
        }

        // Start a quiz by Id
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> Take(string quizId)
        {
            var quizModel = await this.quizzesService.GetQuizByIdAsync<TakeQuizViewModel>(quizId);

            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.quizzesService.StartQuiz(quizId, currentUserId);

            return this.View(quizModel);
        }

        // Get quiz by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> ById(string quizId)
        {
            var quiz = await this.quizzesService.GetQuizByIdAsync<QuizPageViewModel>(quizId);

            return this.View(quiz);
        }
    }
}
