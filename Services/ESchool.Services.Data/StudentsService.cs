namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Student;
    using Microsoft.AspNetCore.Identity;

    public class StudentsService : IStudentsService
    {
        private readonly IDeletableEntityRepository<ClassInSchool> classesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public StudentsService(
            IDeletableEntityRepository<ClassInSchool> classesRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.classesRepository = classesRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Update School by id
        public async Task UpdateAsync(EditStudentViewModel input, string userId)
        {
            var student = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var wantedClass = this.classesRepository
                .All()
                .FirstOrDefault(x => x.Id == input.ClassInSchoolId);

            if (wantedClass != null)
            {
                wantedClass.Students.Add(student);
            }

            await this.classesRepository.SaveChangesAsync();
        }

        // Approve student by Id
        public async Task ApproveStudentAsync(string id)
        {
            var student = await this.userManager.FindByIdAsync(id);
            if (student.IsApproved != true)
            {
                student.IsApproved = true;
            }

            await this.userManager.UpdateAsync(student);
        }

        // Reject student by Id
        public async Task RejectStudentAsync(string id)
        {
            var student = await this.userManager.FindByIdAsync(id);
            if (student.IsApproved)
            {
                student.IsApproved = false;
            }

            await this.userManager.UpdateAsync(student);
        }

        // Add a student to a class
        public async Task AddStudentToClass(string userId, int classId)
        {
            var student = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var wantedClass = this.classesRepository
                .All()
                .FirstOrDefault(x => x.Id == classId);

            if (wantedClass != null)
            {
                wantedClass.Students.Add(student);
            }

            await this.classesRepository.SaveChangesAsync();
        }

        // Get all users
        public IEnumerable<StudentAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20)
        {
            var students = this.userManager.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(GlobalConstants.StudentRoleName))
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<StudentAtListViewModel>()
                .ToList();

            return students;
        }

        // List with students in some school
        public IEnumerable<StudentAtListViewModel> GetAllStudentsInSchool<T>(int schoolId, int page, int studentsPerPage = 20)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.StudentRoleName).Result;

            var students = this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id) && x.SchoolId == schoolId)
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * studentsPerPage).Take(studentsPerPage)
                .To<StudentAtListViewModel>()
                .ToList();

            return students;
        }

        // List with students in some school
        public IEnumerable<StudentAtListViewModel> GetAllStudentsInSchoolForAddingInClass<T>(int schoolId, int page, int itemsPerPage = 20)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.StudentRoleName).Result;

            var students = this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id) && x.SchoolId == schoolId && x.IsApproved == true && x.ClassInSchoolId == null)
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .To<StudentAtListViewModel>()
                .ToList();

            return students;
        }

        // List with students in some class
        public IEnumerable<StudentAtListViewModel> GetAllStudentsInClass<T>(int schoolId, int classInSchoolId, int page, int itemsPerPage = 20)
        {
            var role = this.roleManager.FindByNameAsync(GlobalConstants.StudentRoleName).Result;

            var students = this.userManager.Users
                .Where(x => x.Roles.Any(r => r.RoleId == role.Id) && x.SchoolId == schoolId)
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.SecondName)
                .OrderBy(x => x.LastName)
                .OrderBy(x => x.CreatedOn)
                .To<StudentAtListViewModel>()
                .ToList();

            return students;
        }

        // Get student by its id
        public T GetById<T>(string id)
        {
            var student = this.userManager.Users
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return student;
        }

        // Get user's count
        public int GetCount()
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.StudentRoleName).Result
                .Count();
        }

        // Get students' count in some school
        public int GetCountBySchoolId(int schoolId)
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.StudentRoleName).Result
                .Where(x => x.SchoolId == schoolId)
                .Count();
        }
    }
}
