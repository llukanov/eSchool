namespace ESchool.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class StudentsSeeder : ISeeder
    {
        // Create default students
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var students = new List<ApplicationUser>()
            {
                new ApplicationUser { UserName = "student@eschool.com", Email = "student@eschool.com", FirstName = "Аврора", SecondName = "Спартакова", LastName = "Керемергиева", SchoolId = 1111 },
                new ApplicationUser { UserName = "student2@eschool.com", Email = "student2@eschool.com", FirstName = "Автономка", SecondName = "Торимацова", LastName = "Шестакова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student3@eschool.com", Email = "student3@eschool.com", FirstName = "Аделаид", SecondName = "Димитров", LastName = "Димитров", SchoolId = 1111 },
                new ApplicationUser { UserName = "student4@eschool.com", Email = "student4@eschool.com", FirstName = "Аида", SecondName = "Пелтекова", LastName = "Пелтекова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student5@eschool.com", Email = "student5@eschool.com", FirstName = "Алеман", SecondName = "Клюсов", LastName = "Клюсов", SchoolId = 1111 },
                new ApplicationUser { UserName = "student6@eschool.com", Email = "student6@eschool.com", FirstName = "Анна", SecondName = "Кокошкова", LastName = "Кокошкова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student7@eschool.com", Email = "student7@eschool.com", FirstName = "Ария", SecondName = "Тодорова", LastName = "Тодорова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student8@eschool.com", Email = "student8@eschool.com", FirstName = "Арсени", SecondName = "Чанталиев", LastName = "Чанталиев", SchoolId = 1111 },
                new ApplicationUser { UserName = "student9@eschool.com", Email = "student9@eschool.com", FirstName = "Африкан", SecondName = "Симеонов", LastName = "Симеонов", SchoolId = 1111 },
                new ApplicationUser { UserName = "student10@eschool.com", Email = "student10@eschool.com", FirstName = "Ахил", SecondName = "Карамузов", LastName = "Карамузов", SchoolId = 1111 },
                new ApplicationUser { UserName = "student11@eschool.com", Email = "student11@eschool.com", FirstName = "Бурза", SecondName = "Кехайова", LastName = "Кехайова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student12@eschool.com", Email = "student12@eschool.com", FirstName = "Аврора", SecondName = "Спартакова", LastName = "Керемергиева", SchoolId = 1111 },
                new ApplicationUser { UserName = "student13@eschool.com", Email = "student13@eschool.com", FirstName = "Автономка", SecondName = "Торимацова", LastName = "Шестакова", SchoolId = 1111 },
                new ApplicationUser { UserName = "student14@eschool.com", Email = "student14@eschool.com", FirstName = "Аделаид", SecondName = "Димитров", LastName = "Димитров", SchoolId = 1111 },
                new ApplicationUser { UserName = "student15@eschool.com", Email = "student15@eschool.com", FirstName = "Аида", SecondName = "Пелтекова", LastName = "Пелтекова", SchoolId = 1111 },
            };

            foreach (var student in students)
            {
                await SeedStudentAsync(userManager, student);
                await SeedStudentToRoleAsync(roleManager, userManager, student.Email, GlobalConstants.StudentRoleName);
            }
        }

        // Create student
        private static async Task SeedStudentAsync(UserManager<ApplicationUser> userManager, ApplicationUser admin)
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(admin, GlobalConstants.StudentPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        // Add a student to a Role
        private static async Task SeedStudentToRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string userEmail, string roleName)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                var user = await userManager.FindByEmailAsync(userEmail);
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
