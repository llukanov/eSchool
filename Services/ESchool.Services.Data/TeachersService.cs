namespace ESchool.Services.Data
{
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Teacher;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using ESchool.Common;
    using System.Threading.Tasks;

    public class TeachersService : ITeachersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public TeachersService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Approve teacher by Id
        public async Task ApproveTeacherAsync(string id)
        {
            var teacher = await this.userManager.FindByIdAsync(id);
            if (teacher.IsApproved != true)
            {
                teacher.IsApproved = true;
            }

            await this.userManager.UpdateAsync(teacher);
        }

        // Reject teacher by Id
        public async Task RejectTeacherAsync(string id)
        {
            var teacher = await this.userManager.FindByIdAsync(id);
            if (teacher.IsApproved)
            {
                teacher.IsApproved = false;
            }

            await this.userManager.UpdateAsync(teacher);
        }

        // List with teachers - getting all ones
        public IEnumerable<TeacherAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20)
        {
            var teachers = this.userManager.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(GlobalConstants.TeacherRoleName))
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<TeacherAtListViewModel>()
                .ToList();

            return teachers;
        }

        // List with teachers in some school
        public IEnumerable<TeacherAtListViewModel> GetAllTeachersInSchool<T>(int schoolId, int page, int itemsPerPage = 20)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.TeacherRoleName).Result;

            var teachers = this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id) && x.SchoolId == schoolId)
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .To<TeacherAtListViewModel>()
                .ToList();

            return teachers;
        }

        // Get teacher by id
        public T GetById<T>(string id)
        {
            var teacher = this.userManager.Users
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return teacher;
        }

        // Get teacher's count
        public int GetCount()
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.TeacherRoleName).Result
                .Count();
        }

        // Get teachers' count in some school
        public int GetCountBySchoolId(int schoolId)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.TeacherRoleName).Result;

            return this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id) && x.SchoolId == schoolId)
                .Count();
        }
    }
}
