namespace ESchool.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.AssignmentReply;
    using ESchool.Web.ViewModels.Subject;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AssignmentController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly ISubjectsService subjectsService;
        private readonly IAssignmentRepliesService assignmentRepliesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public AssignmentController(
            IAssignmentsService assignmentsService,
            ISubjectsService subjectsService,
            IAssignmentRepliesService assignmentRepliesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.assignmentsService = assignmentsService;
            this.subjectsService = subjectsService;
            this.assignmentRepliesService = assignmentRepliesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        // Create assignment
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Create(string teacherId, int lessonId)
        {
            var viewModel = new CreateAssignmentInputModel();

            viewModel.LessonId = lessonId;
            viewModel.TeacherId = teacherId;

            if (viewModel.LessonId == 0 || viewModel.TeacherId == null)
            {
                return this.RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(CreateAssignmentInputModel input)
        {
            try
            {
                await this.assignmentsService.CreateAsync(input, $"{this.environment.WebRootPath}/materials");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Задачата е добавена.";

            return this.RedirectToAction(actionName: "ById", controllerName: "Lesson", new { lessonId = input.LessonId });
        }

        // Edit assignment by its Id
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Edit(string assignmentId)
        {
            var inputModel = this.assignmentsService.GetById<EditAssignmentInputModel>(assignmentId);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Edit(string assignmentId, EditAssignmentInputModel input)
        {
            try
            {
                await this.assignmentsService.UpdateAsync(input, assignmentId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), new { assignmentId });
        }

        // Get all assignment in some subject
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public IActionResult AllInSubject(int subjectId, int id = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 10;
            var viewModel = new AllAssignmentsInSubjectViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.assignmentsService.GetCountInSubject(subjectId),
                SubjectId = subjectId,
                Subject = this.subjectsService.GetById<SubjectAtListViewModel>(subjectId),
                Assignments = this.assignmentsService.GetAllInSubject<AssignmentAtListViewModel>(subjectId, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        // Get specific assignment by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public IActionResult ById(string assignmentId)
        {
            if (this.User.IsInRole(GlobalConstants.StudentRoleName))
            {
                var assignment = this.assignmentsService.GetById<AssignmentPageViewModel>(assignmentId);

                return this.View(assignment);
            }
            else
            {
                var assignment = this.assignmentsService.GetById<AssignmentPageForTeacherViewModel>(assignmentId);

                assignment.AssignmentReplies = this.assignmentRepliesService.GetAllRepliesOfAssignment<AssignmentReplyAtListViewModel>(assignmentId);

                return this.View("ByIdForTeacher", assignment);
            }
        }
    }
}
