namespace ESchool.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class SuperAdminSeeder : ISeeder
    {
        // Create default Super Admin
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var superAdmin = new ApplicationUser()
            {
                UserName = "superadmin@eschool.com",
                Email = "superadmin@eschool.com",
                SchoolId = 1110,
            };

            await SeedSuperAdminAsync(userManager, superAdmin);
            await SeedSuperAdminToRoleAsync(roleManager, userManager, superAdmin.Email, GlobalConstants.SuperAdminRoleName);
        }

        // Create Super Admin
        private static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, ApplicationUser superAdmin)
        {
            var user = await userManager.FindByEmailAsync(superAdmin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(superAdmin, GlobalConstants.SuperAdminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        // Add Super Admin to Role
        private static async Task SeedSuperAdminToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string superAdminEmail, string superAdminRoleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(superAdminRoleName);
            if (roleExist)
            {
                var user = await userManager.FindByEmailAsync(superAdminEmail);
                var isUserInRole = await userManager.IsInRoleAsync(user, superAdminRoleName);
                if (user != null && !isUserInRole)
                {
                    var result = await userManager.AddToRoleAsync(user, superAdminRoleName);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
