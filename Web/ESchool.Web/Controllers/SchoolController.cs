namespace ESchool.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.School;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SchoolController : Controller
    {
        private readonly ISchoolsService schoolsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SchoolController(ISchoolsService schoolsService, UserManager<ApplicationUser> userManager)
        {
            this.schoolsService = schoolsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateSchoolInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public async Task<IActionResult> Create(CreateSchoolInputModel input)
        {
            try
            {
                await this.schoolsService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Училището е добавено.";

            return this.RedirectToAction(nameof(this.All));
        }

        // Edit school's info
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.schoolsService.GetById<EditSchoolInputModel>(id);
            return this.View("Edit", inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public async Task<IActionResult> Edit(int id, EditSchoolInputModel input)
        {
            try
            {
                await this.schoolsService.UpdateAsync(input, id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Училището е изтрито!";

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        // Delete school
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = this.userManager.Users
                .Where(x => x.SchoolId == id)
                .FirstOrDefault();

            if (user == null)
            {
                await this.schoolsService.DeleteAsync(id);

                this.TempData["Message"] = "Училището е изтрито!";
            }
            else
            {
                this.TempData["Message"] = "Училището не може да бъде изтрито, тъй като в него има регистрирани потребители!";
            }

            return this.RedirectToAction(nameof(this.All));
        }

        // Get list with all schools
        [HttpGet]
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 10;
            var viewModel = new AllSchoolsViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.schoolsService.GetCount(),
                Schools = this.schoolsService.GetAll<SchoolAtListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        // Get specific school by its id
        [HttpGet]
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public IActionResult ById(int id)
        {
            var school = this.schoolsService.GetById<SchoolPageViewModel>(id);

            school.AdminName = this.schoolsService.GetAdminName(school.Id);
            school.TeachersCount = this.schoolsService.GetTeachersCount(school.Id);
            school.StudentsCount = this.schoolsService.GetStudentsCount(school.Id);

            return this.View(school);
        }
    }
}
