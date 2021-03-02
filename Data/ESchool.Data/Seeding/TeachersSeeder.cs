namespace ESchool.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class TeachersSeeder : ISeeder
    {
        // Create default Teachers
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var teachers = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "teacher@eschool.com", Email = "teacher@eschool.com", FirstName = "Аврора", SecondName = "Спартакова", LastName = "Керемергиева", SchoolId = 1111 },
                new ApplicationUser { UserName = "teacher2@eschool.com", Email = "teacher2@eschool.com", FirstName = "Автономка", SecondName = "Торимацова", LastName = "Шестакова", SchoolId = 1111 },
                new ApplicationUser { UserName = "teacher3@eschool.com", Email = "teacher3@eschool.com", FirstName = "Аделаид", SecondName = "Димитров", LastName = "Димитров", SchoolId = 1111 },
                new ApplicationUser { UserName = "teacher4@eschool.com", Email = "teacher4@eschool.com", FirstName = "Аида", SecondName = "Пелтекова", LastName = "Пелтекова", SchoolId = 1111 },
                new ApplicationUser { UserName = "teacher5@eschool.com", Email = "teacher5@eschool.com", FirstName = "Алеман", SecondName = "Клюсов", LastName = "Клюсов", SchoolId = 1111 },
            };

            foreach (var teacher in teachers)
            {
                await SeedTeacherAsync(userManager, teacher);
                await SeedTeacherToRoleAsync(roleManager, userManager, teacher.Email, GlobalConstants.TeacherRoleName);
            }
        }

        // Create Teacher
        private static async Task SeedTeacherAsync(UserManager<ApplicationUser> userManager, ApplicationUser admin)
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(admin, GlobalConstants.TeacherPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        // Add a Teacher to a Role
        private static async Task SeedTeacherToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string adminEmail, string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                var user = await userManager.FindByEmailAsync(adminEmail);
                var isUserInRole = await userManager.IsInRoleAsync(user, roleName);
                if (user != null && !isUserInRole)
                {
                    var result = await userManager.AddToRoleAsync(user, roleName);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
