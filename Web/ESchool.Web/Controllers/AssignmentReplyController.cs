using ESchool.Common;
using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Assignment;
using ESchool.Web.ViewModels.AssignmentReply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESchool.Web.Controllers
{
    public class AssignmentReplyController : Controller
    {
        private readonly IAssignmentsService assignmentsService;
        private readonly IAssignmentRepliesService assignmentRepliesService;
        private readonly IGradesService gradesService;
        private readonly IDeletableEntityRepository<AssignmentReply> assignmentsRepliesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public AssignmentReplyController(
            IAssignmentsService assignmentsService,
            IAssignmentRepliesService assignmentRepliesService,
            IGradesService gradesService,
            IDeletableEntityRepository<AssignmentReply> assignmentsRepliesRepository,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.assignmentsService = assignmentsService;
            this.assignmentRepliesService = assignmentRepliesService;
            this.gradesService = gradesService;
            this.assignmentsRepliesRepository = assignmentsRepliesRepository;
            this.userManager = userManager;
            this.environment = environment;
        }

        // Send assignment
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult Send(string assignmentId)
        {
            var reply = this.assignmentsRepliesRepository
                .AllAsNoTracking()
                .Where(x => x.AssignmentId == assignmentId && x.StudentId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault();

            if (reply != null)
            {
                this.TempData["Message"] = "Вече сте добавили отговор към тази задача!";

                return this.RedirectToAction(actionName: nameof(this.AllOfStudent), new { studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }

            var assignment = this.assignmentsService.GetById<AssignmentPageViewModel>(assignmentId);

            var viewModel = new SendAssignmentReplyInputModel();

            viewModel.AssignmentId = assignment.Id;
            viewModel.Description = assignment.Description;
            viewModel.Lesson = assignment.Lesson;
            viewModel.Materials = assignment.Materials;
            viewModel.Teacher = assignment.Teacher;
            viewModel.StudentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.Deadline = assignment.Deadline;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<IActionResult> Send(string assignmentId, SendAssignmentReplyInputModel input)
        {
            try
            {
                var assignment = this.assignmentsService.GetById<AssignmentPageViewModel>(assignmentId);

                input.AssignmentId = assignment.Id;
                input.Description = assignment.Description;
                input.Lesson = assignment.Lesson;
                input.Materials = assignment.Materials;
                input.Teacher = assignment.Teacher;
                input.StudentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                input.Deadline = assignment.Deadline;

                await this.assignmentRepliesService.SendAsync(input, $"{this.environment.WebRootPath}/materials");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Отговорът е добавен.";

            return this.RedirectToAction(actionName: "ById", controllerName: "Lesson", new { lessonId = input.Lesson.Id });
        }

        // Return assignment
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Return(string assignmentReplyId)
        {
            var assignmentReply = this.assignmentRepliesService.GetById<ReturnAssignmentReplyInputModel>(assignmentReplyId);

            assignmentReply.Grades = this.gradesService.GetAllAsKeyValuePairs();

            return this.View(assignmentReply);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Return(string assignmentReplyId, ReturnAssignmentReplyInputModel input)
        {
            var teacherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                input.Grades = this.gradesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.assignmentRepliesService.UpdateAsync(input, assignmentReplyId, teacherId);

            var assignmentReply = this.assignmentRepliesService.GetById<ReturnAssignmentReplyInputModel>(assignmentReplyId);

            this.TempData["Message"] = "Отговорът е добавен.";

            return this.RedirectToAction(actionName: "ById", controllerName: "Assignment", new { assignmentId = assignmentReply.AssignmentId });
        }

        // Get all assignment replies of student
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult AllOfStudent(string studentId, int id = 1)
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
            var viewModel = new AllAssignmentsRepliesOfStudentViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.assignmentRepliesService.GetCountRepliesOfStudent(studentId),
                StudentId = studentId,
                AssignmentsReplies = this.assignmentRepliesService.GetAllRepliesOfStudent<AssignmentReplyAtListViewModel>(studentId, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
