using ESchool.Common;
using ESchool.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Data.Seeding
{
    internal class AdminsSeeder : ISeeder
    {
        // Create default Admins
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var admins = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "admin@eschool.com", Email = "admin@eschool.com", FirstName = "Аврора", SecondName = "Спартакова", LastName = "Керемергиева", SchoolId = 1111 },
                new ApplicationUser { UserName = "admin2@eschool.com", Email = "admin2@eschool.com", FirstName = "Автономка", SecondName = "Торимацова", LastName = "Шестакова", SchoolId = 1112 },
                new ApplicationUser { UserName = "admin3@eschool.com", Email = "admin3@eschool.com", FirstName = "Аделаид", SecondName = "Димитров", LastName = "Димитров", SchoolId = 1113 },
                new ApplicationUser { UserName = "admin4@eschool.com", Email = "admin4@eschool.com", FirstName = "Аида", SecondName = "Пелтекова", LastName = "Пелтекова", SchoolId = 1114 },
                new ApplicationUser { UserName = "admin5@eschool.com", Email = "admin5@eschool.com", FirstName = "Алеман", SecondName = "Клюсов", LastName = "Клюсов", SchoolId = 1115 },
            };

            foreach (var admin in admins)
            {
                await SeedAdminAsync(userManager, admin);
                await SeedAdminToRoleAsync(roleManager, userManager, admin.Email, GlobalConstants.AdminRoleName);
            }
        }

        // Create Admin
        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, ApplicationUser admin)
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(admin, GlobalConstants.AdminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        // Add Admin to Role
        private static async Task SeedAdminToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string adminEmail, string adminRoleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(adminRoleName);
            if (roleExist)
            {
                var user = await userManager.FindByEmailAsync(adminEmail);
                var isUserInRole = await userManager.IsInRoleAsync(user, adminRoleName);
                if (user != null && !isUserInRole)
                {
                    var result = await userManager.AddToRoleAsync(user, adminRoleName);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
