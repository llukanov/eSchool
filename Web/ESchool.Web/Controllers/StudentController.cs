using ESchool.Common;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Web.ViewModels.Student;
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
    public class StudentController : Controller
    {
        private readonly IStudentsService studentsService;
        private readonly IClassesService classesService;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(
            IStudentsService studentsService,
            IClassesService classesService,
            UserManager<ApplicationUser> userManager)
        {
            this.studentsService = studentsService;
            this.classesService = classesService;
            this.userManager = userManager;
        }

        // Approve student by id
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> ApproveStudent(int schoolId, string id)
        {
            try
            {
                await this.studentsService.ApproveStudentAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
            }

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
        }

        // Reject student by id
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> RejectStudent(int schoolId, string id)
        {
            try
            {
                await this.studentsService.RejectStudentAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
            }

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
        }

        // Add student to a class
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> AddStudentToClass(int schoolId, int classId, string userId)
        {
            try
            {
                await this.studentsService.AddStudentToClass(userId, classId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
            }

            return this.RedirectToAction(actionName: "ById", controllerName: "Class", new { classInSchoolId = classId });
        }

        // Get all students - it is used in admin panel
        [Authorize(Roles = GlobalConstants.AdminRoleName + "," + GlobalConstants.TeacherRoleName)]
        public IActionResult AllInSchool(int schoolId, int id = 1)
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
            var viewModel = new AllStudentsInSchoolViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.studentsService.GetCountBySchoolId(user.SchoolId),
                SchoolId = user.SchoolId,
                Students = this.studentsService.GetAllStudentsInSchool<StudentAtListViewModel>(user.SchoolId, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
