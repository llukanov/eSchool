using ESchool.Common;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Grade;
using ESchool.Web.ViewModels.Student;
using ESchool.Web.ViewModels.Subject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESchool.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISubjectsService subjectsService;
        private readonly IStudentGradesServices studentGradesServices;
        private readonly IGradesService gradesService;
        private readonly IStudentsService studentsService;

        public GradeController(
            UserManager<ApplicationUser> userManager,
            ISubjectsService subjectsService,
            IStudentGradesServices studentGradesServices,
            IGradesService gradesService,
            IStudentsService studentsService)
        {
            this.userManager = userManager;
            this.subjectsService = subjectsService;
            this.studentGradesServices = studentGradesServices;
            this.gradesService = gradesService;
            this.studentsService = studentsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Get all grades of students in a subject
        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public IActionResult AllInSubject(int subjectId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            var viewModel = new AllGradesInSubjectViewModel
            {
                SubjectId = subjectId,
                Subject = this.subjectsService.GetById<SubjectAtListViewModel>(subjectId),
                Students = this.studentGradesServices.GetAllInSubject<StudentGradeAtListViewModel>(subjectId),
                Grades = this.gradesService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        // Get all grades of a student
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public IActionResult AllOfStudent(string studentId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var student = this.userManager.Users
                .Where(x => x.Id == studentId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            var viewModel = new AllGradesOfStudentsViewModel
            {
                Student = this.studentsService.GetById<StudentAtListViewModel>(studentId),
                Grades = this.studentGradesServices.GetAllOfStudents<GradesOfStudentAtListViewModel>(studentId),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.TeacherRoleName)]
        public async Task<IActionResult> Add(int subjectId, string studentId, int gradeId)
        {
            var teacherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.studentGradesServices.AddGrade(subjectId, studentId, teacherId, gradeId);

            return this.RedirectToAction(nameof(this.AllInSubject), new { subjectId = subjectId });
        }
    }
}
