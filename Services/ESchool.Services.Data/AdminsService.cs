namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Admin;
    using Microsoft.AspNetCore.Identity;

    public class AdminsService : IAdminsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdminsService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Approve admin by Id
        public async Task ApproveAdminAsync(string id)
        {
            var admin = await this.userManager.FindByIdAsync(id);
            if (admin.IsApproved != true)
            {
                admin.IsApproved = true;
            }

            await this.userManager.UpdateAsync(admin);
        }

        // Reject admin by Id
        public async Task RejectAdminAsync(string id)
        {
            var admin = await this.userManager.FindByIdAsync(id);
            if (admin.IsApproved)
            {
                admin.IsApproved = false;
            }

            await this.userManager.UpdateAsync(admin);
        }

        // Get all admins
        public IEnumerable<AdminAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.AdminRoleName).Result;

            var admins = this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id))
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<AdminAtListViewModel>()
                .ToList();

            return admins;
        }

        // Get specicific admin by id
        public T GetById<T>(string id)
        {
            var admin = this.userManager.Users
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return admin;
        }

        // Get admin's count
        public int GetCount()
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.AdminRoleName).Result
                .Count();
        }

        // Get school's name
    }
}
