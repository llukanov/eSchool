namespace ESchool.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using ESchool.Common;
    using ESchool.Data;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels;
    using ESchool.Web.ViewModels.Class;
    using ESchool.Web.ViewModels.Home;
    using ESchool.Web.ViewModels.Subject;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISchoolsService schoolsService;
        private readonly IClassesService classesService;
        private readonly IStudentsService studentsService;
        private readonly ITeachersService teachersService;
        private readonly IAdminsService adminsService;
        private readonly ISubjectsService subjectsService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            ISchoolsService schoolsService,
            IClassesService classesService,
            IStudentsService studentsService,
            ITeachersService teachersService,
            IAdminsService adminsService,
            ISubjectsService subjectsService)
        {
            this.userManager = userManager;
            this.schoolsService = schoolsService;
            this.classesService = classesService;
            this.studentsService = studentsService;
            this.teachersService = teachersService;
            this.adminsService = adminsService;
            this.subjectsService = subjectsService;
        }

        [Authorize]
        public IActionResult Index()
        {
            // Super Admin Index
            if (this.User.IsInRole(GlobalConstants.SuperAdminRoleName))
            {
                var viewModel = new SuperAdminIndexViewModel
                {
                    SchoolsCount = this.schoolsService.GetCount(),
                    AdminsCount = this.adminsService.GetCount(),
                    StudentsCount = this.studentsService.GetCount(),
                    TeachersCount = this.teachersService.GetCount(),
                };
                return this.View("SuperAdminIndex", viewModel);
            }

            // Admin Index
            else if (this.User.IsInRole(GlobalConstants.AdminRoleName))
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = this.userManager.FindByIdAsync(currentUserId).Result;

                var school = this.schoolsService.GetSchoolByUserId(currentUserId);

                var viewModel = new AdminIndexViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FirstName + " " + currentUser.LastName,
                    SchoolId = school.Id,
                    School = school,
                    SchoolName = school.Name,
                    IsApproved = currentUser.IsApproved,
                    TeachersCount = this.teachersService.GetCountBySchoolId(school.Id),
                    StudentsCount = this.studentsService.GetCountBySchoolId(school.Id),
                    ClassesCount = this.classesService.GetCountBySchoolId(school.Id),
                };

                if (currentUser.IsApproved)
                {
                    return this.View("AdminIndex", viewModel);
                }
                else
                {
                    return this.View("UnApproved");
                }
            }

            // Teacher Index
            else if (this.User.IsInRole(GlobalConstants.TeacherRoleName))
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = this.userManager.FindByIdAsync(currentUserId).Result;

                var school = this.GetSchool(currentUserId);

                var viewModel = new TeacherIndexViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FirstName + " " + currentUser.LastName,
                    SchoolId = school.Id,
                    SchoolName = school.Name,
                    IsApproved = currentUser.IsApproved,
                    ClassesCount = this.classesService.GetCountBySchoolId(school.Id),
                    Subjects = this.subjectsService.GetAllSubjectOfTeacher<SubjectAtListViewModel>(currentUserId),
                };

                if (currentUser.IsApproved)
                {
                    return this.View("TeacherIndex", viewModel);
                }
                else
                {
                    return this.View("UnApproved");
                }
            }

            // Student Index Model
            else
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = this.userManager.FindByIdAsync(currentUserId).Result;

                var school = this.GetSchool(currentUserId);

                var viewModel = new StudentIndexViewModel
                {
                    Id = currentUser.Id,
                    FullName = currentUser.FirstName + " " + currentUser.LastName,
                    ClassInSchool = this.classesService.GetClassOfStudent<ClassInSchool>(currentUser),
                    SchoolId = school.Id,
                    SchoolName = school.Name,
                    IsApproved = currentUser.IsApproved,
                    Subjects = this.subjectsService.GetAllSubjectsOfStudent<SubjectAtListViewModel>(currentUser),
                };

                if (currentUser.IsApproved)
                {
                    return this.View("StudentIndex", viewModel);
                }
                else
                {
                    return this.View("UnApproved");
                }
            }
        }

        // Get the school of current user (admin, teachet or student)
        private School GetSchool(string currentUserId)
        {
            return this.schoolsService.GetSchoolByUserId(currentUserId);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
