namespace ESchool.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Teacher;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TeacherController : Controller
    {
        private readonly ITeachersService teachersService;
        private readonly UserManager<ApplicationUser> userManager;

        public TeacherController(
            ITeachersService teachersService,
            UserManager<ApplicationUser> userManager)
        {
            this.teachersService = teachersService;
            this.userManager = userManager;
        }

        // Approve teacher by id
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> ApproveTeacher(int schoolId, string id)
        {
            try
            {
                await this.teachersService.ApproveTeacherAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
            }

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
        }

        // Reject teacher by id
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> RejectTeacher(int schoolId, string id)
        {
            try
            {
                await this.teachersService.RejectTeacherAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
            }

            return this.RedirectToAction(nameof(this.AllInSchool), new { schoolId = schoolId });
        }

        // Get all teachers in schools
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

            const int ItemsPerPage = 20;
            var viewModel = new AllTeachersInSchoolViewModel
            {
                ItemsCount = this.teachersService.GetCountBySchoolId(user.SchoolId),
                Teachers = this.teachersService.GetAllTeachersInSchool<TeacherAtListViewModel>(user.SchoolId, id, ItemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
