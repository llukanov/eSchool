namespace ESchool.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.Lesson;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class LessonController : Controller
    {
        private readonly ILessonsService lessonsService;
        private readonly IAssignmentsService assignmentsService;
        private readonly IWebHostEnvironment environment;

        public LessonController(
            ILessonsService lessonsService,
            IAssignmentsService assignmentsService,
            IWebHostEnvironment environment)
        {
            this.lessonsService = lessonsService;
            this.assignmentsService = assignmentsService;
            this.environment = environment;
        }

        // Add Lesson in current subject from current teacher
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Create(int subjectId, string teacherId)
        {
            var viewModel = new CreateLessonInputModel();

            viewModel.SubjectId = subjectId;
            viewModel.TeacherId = teacherId;

            if (viewModel.SubjectId == 0 || viewModel.TeacherId == null)
            {
                return this.RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(CreateLessonInputModel input)
        {
            try
            {
                await this.lessonsService.CreateAsync(input, $"{this.environment.WebRootPath}/materials");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Темата е добавено.";

            return this.RedirectToAction(actionName: "ById", controllerName: "Subject", new { subjectId = input.SubjectId });
        }

        // Edit lesson by its Id
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Edit(int lessonId)
        {
            var inputModel = this.lessonsService.GetById<EditLessonInputModel>(lessonId);

            return this.View("Edit", inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Edit(int lessonId, EditLessonInputModel input)
        {
            try
            {
                await this.lessonsService.UpdateAsync(input, lessonId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), new { lessonId });
        }

        // Get specific lesson by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public IActionResult ById(int lessonId)
        {
            var lesson = this.lessonsService.GetById<LessonPageViewModel>(lessonId);

            lesson.Assignments = this.assignmentsService.GetAllAssignmensInLesson<AssignmentAtListViewModel>(lessonId);

            return this.View(lesson);
        }
    }
}
