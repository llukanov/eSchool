namespace ESchool.Web.Controllers
{
    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Class;
    using ESchool.Web.ViewModels.Student;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ClassController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private readonly IClassesService classesService;
        private readonly IStudentsService studentsService;
        private readonly ISchoolsService schoolsService;
        private readonly IDeletableEntityRepository<ClassInSchool> classRepository;
        private readonly IDeletableEntityRepository<Subject> subjectRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ClassController(
            IClassesService classesService,
            IStudentsService studentsService,
            ISchoolsService schoolsService,
            IDeletableEntityRepository<ClassInSchool> classRepository,
            IDeletableEntityRepository<Subject> subjectRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.classesService = classesService;
            this.studentsService = studentsService;
            this.schoolsService = schoolsService;
            this.classRepository = classRepository;
            this.subjectRepository = subjectRepository;
            this.userManager = userManager;
        }

        // Create a new class
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateClassInputModel();

            var adminId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = this.userManager.Users
                .Where(x => x.Id == adminId)
                .FirstOrDefault();

            if (!admin.IsApproved)
            {
                return this.View("UnApproved");
            }

            viewModel.SchoolId = admin.SchoolId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Create(CreateClassInputModel input)
        {
            try
            {
                await this.classesService.CreateAsync(input, input.SchoolId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Класът е добавен.";

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = input.SchoolId });
        }

        // Edit class's info
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult Edit(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            var inputModel = this.classesService.GetById<EditClassInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Edit(int id, EditClassInputModel input)
        {
            try
            {
                await this.classesService.UpdateAsync(input, id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        // Delete class
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(int id, int schoolId)
        {
            var classInSchool = this.subjectRepository
                .AllAsNoTracking()
                .Where(x => x.ClassId == id)
                .FirstOrDefault();

            if (classInSchool == null)
            {
                await this.classesService.DeleteAsync(id);

                this.TempData["Message"] = "Класът е изтрит!";
            }
            else
            {
                this.TempData["Message"] = "Класът не може да бъде изтрит, тъй като в него вече има създаден предмети!";
            }

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
        }

        // Get all classes in some school
        [Authorize(Roles = GlobalConstants.AdminRoleName + "," + GlobalConstants.TeacherRoleName)]
        public IActionResult AllInSchool(int schoolId, int id = 1)
        {
            var adminId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = this.userManager.Users
                .Where(x => x.Id == adminId)
                .FirstOrDefault();

            if (!admin.IsApproved)
            {
                return this.View("UnApproved");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new AllClassesInSchoolViewModel
            {
                ItemsCount = this.classesService.GetCountBySchoolId(admin.SchoolId),
                Classes = this.classesService.GetAllClassesInSchool<ClassAtListViewModel>(admin.SchoolId),
            };
            return this.View(viewModel);
        }

        // Get specific class by its id
        [Authorize(Roles = GlobalConstants.AdminRoleName + "," + GlobalConstants.TeacherRoleName)]
        public IActionResult ById(int classInSchoolId, int id = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            var classInSchool = this.classesService.GetById<ClassPageViewModel>(classInSchoolId);

            classInSchool.StudentsInSchool = this.studentsService.GetAllStudentsInSchoolForAddingInClass<StudentAtListViewModel>(user.SchoolId, 1, 20);

            if (this.User.IsInRole(GlobalConstants.AdminRoleName))
            {
                return this.View("ByIdForAdmin", classInSchool);
            }
            else
            {
                return this.View("ByIdForTeacher", classInSchool);
            }
        }
    }
}
