namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Home;
    using ESchool.Web.ViewModels.School;
    using Microsoft.AspNetCore.Identity;

    public class SchoolsService : ISchoolsService
    {
        private readonly IDeletableEntityRepository<School> schoolsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public SchoolsService(
            IDeletableEntityRepository<School> schoolsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.schoolsRepository = schoolsRepository;
            this.userManager = userManager;
        }

        // Create School
        public async Task CreateAsync(CreateSchoolInputModel input)
        {
            var school = new School
            {
                Name = input.Name,
                Settlement = input.Settlement,
                Province = input.Province,
            };

            await this.schoolsRepository.AddAsync(school);
            await this.schoolsRepository.SaveChangesAsync();
        }

        // Update School by id
        public async Task UpdateAsync(EditSchoolInputModel input, int id)
        {
            var school = this.schoolsRepository.All().FirstOrDefault(x => x.Id == id);
            school.Name = input.Name;
            await this.schoolsRepository.SaveChangesAsync();
        }

        // Delete School by id
        public async Task DeleteAsync(int id)
        {
            var school = this.schoolsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);
            this.schoolsRepository.Delete(school);
            await this.schoolsRepository.SaveChangesAsync();
        }

        // Add user to school by id
        public async Task AddUserToSchool(ApplicationUser user, int schoolId)
        {
            var school = this.schoolsRepository
                .All()
                .FirstOrDefault(x => x.Id == schoolId);

            if (school != null)
            {
                user.SchoolId = schoolId;
            }

            await this.schoolsRepository.SaveChangesAsync();
        }

        // List with schools - getting all ones
        public IEnumerable<SchoolAtListViewModel> GetAll<T>(int page, int schoolsPerPage = 20)
        {
            var schools = this.schoolsRepository
                .AllAsNoTracking()
                .Where(x => x.Id != GlobalConstants.DefaultSchoolId)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * schoolsPerPage).Take(schoolsPerPage)
                .To<SchoolAtListViewModel>()
                .ToList();
            return schools;
        }

        // Get specific school by its id
        public T GetById<T>(int id)
        {
            var school = this.schoolsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return school;
        }

        // Get school's count
        public int GetCount()
        {
            return this.schoolsRepository
                .All()
                .Where(x => x.Id != GlobalConstants.DefaultSchoolId)
                .Count();
        }

        // Get school's name
        public School GetSchoolByUserId(string userId)
        {
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            return this.schoolsRepository.AllAsNoTracking()
                .Where(x => x.Id == user.SchoolId)
                .FirstOrDefault();
        }

        // Get school's admin
        public string GetAdminName(int schoolId)
        {
            var admin = this.userManager.Users
                .Where(x => x.SchoolId == schoolId)
                .FirstOrDefault();

            if (admin != null)
            {
                return admin.FirstName + " " + admin.LastName;
            }
            else
            {
                return "Няма администратор";
            }
        }

        // Get count of school's teachers
        public int GetTeachersCount(int schoolId)
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.TeacherRoleName).Result
                .Where(x => x.SchoolId == schoolId)
                .Count();
        }

        // Get count of school's students
        public int GetStudentsCount(int schoolId)
        {
            return this.userManager
                .GetUsersInRoleAsync(GlobalConstants.StudentRoleName).Result
                .Where(x => x.SchoolId == schoolId)
                .Count();
        }
    }
}
