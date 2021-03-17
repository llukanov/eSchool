namespace ESchool.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Subject;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SubjectController : Controller
    {
        private readonly IClassesService classesService;
        private readonly IStudentsService studentsService;
        private readonly ISubjectsService subjectsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubjectController(
            IClassesService classesService,
            IStudentsService studentsService,
            ISubjectsService subjectsService,
            UserManager<ApplicationUser> userManager)
        {
            this.classesService = classesService;
            this.studentsService = studentsService;
            this.subjectsService = subjectsService;
            this.userManager = userManager;
        }

        // Create subject
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult Create(int classInSchoolId)
        {

            var viewModel = new CreateSubjectInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Create(CreateSubjectInputModel input, int classInSchoolId, string teacherId)
        {
            try
            {
                await this.subjectsService.CreateAsync(input, classInSchoolId, teacherId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Предметът е добавен.";

            return this.RedirectToAction(actionName: "ById", controllerName: "Class", new { classInSchoolId = classInSchoolId });
        }

        // Get specific subject by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult AllSubjectsOfStudent()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var viewModel = new AllSubjectsViewModel
            {
                Subjects = this.subjectsService.GetAllSubjectsOfStudent<AllSubjectsViewModel>(student),
            };

            return this.View(viewModel);
        }

        // Get specific subject by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public IActionResult ById(int subjectId)
        {
            var subject = this.subjectsService.GetById<SubjectPageViewModel>(subjectId);

            return this.View(subject);
        }
    }
}
