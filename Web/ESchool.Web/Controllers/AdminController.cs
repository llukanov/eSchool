namespace ESchool.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Admin;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : Controller
    {
        private readonly IAdminsService adminsService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(
            IAdminsService adminsService,
            UserManager<ApplicationUser> userManager)
        {
            this.adminsService = adminsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        // Approve admin
        [HttpPost]
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public async Task<IActionResult> ApproveAdmin(string id)
        {
            try
            {
                await this.adminsService.ApproveAdminAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.All));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        // Approve admin
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public async Task<IActionResult> RejectAdmin(string id)
        {
            try
            {
                await this.adminsService.RejectAdminAsync(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);

                return this.RedirectToAction(nameof(this.All));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        // Get admins
        [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 20;
            var viewModel = new AllAdminsViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.adminsService.GetCount(),
                Admins = this.adminsService.GetAll<AdminAtListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
