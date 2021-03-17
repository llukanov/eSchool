namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Class;
    using Microsoft.AspNetCore.Identity;

    public class ClassesService : IClassesService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ClassInSchool> classesRepository;
        private readonly ISchoolsService schoolsService;

        public ClassesService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ClassInSchool> classesRepository,
            ISchoolsService schoolsService)
        {
            this.userManager = userManager;
            this.classesRepository = classesRepository;
            this.schoolsService = schoolsService;
        }

        // Create new class by input model and SchoolId
        public async Task CreateAsync(CreateClassInputModel input, int schoolId)
        {
            var newClass = new ClassInSchool
            {
                Name = input.Name,
                SchoolId = input.SchoolId,
            };

            await this.classesRepository.AddAsync(newClass);
            await this.classesRepository.SaveChangesAsync();
        }

        // Edit a class by id
        public async Task UpdateAsync(EditClassInputModel input, int id)
        {
            var editClass = this.classesRepository
                .All()
                .FirstOrDefault(x => x.Id == id);
            editClass.Name = input.Name;
            await this.classesRepository.SaveChangesAsync();
        }

        // Delete a class by id
        public async Task DeleteAsync(int id)
        {
            var classForDeletion = this.classesRepository
                .All()
                .FirstOrDefault(x => x.Id == id);
            this.classesRepository.Delete(classForDeletion);
            await this.classesRepository.SaveChangesAsync();
        }

        // Get class of student
        public ClassInSchool GetClassOfStudent<T>(ApplicationUser student)
        {
            var classInSchool = this.classesRepository
                .AllAsNoTracking()
                .Where(x => x.Students.Contains(student))
                .FirstOrDefault();

            return classInSchool;
        }

        // List with classes in some school
        public IEnumerable<ClassAtListViewModel> GetAllClassesInSchool<T>(int schoolId)
        {
            var classes = this.classesRepository
                .AllAsNoTracking()
                .Where(x => x.SchoolId == schoolId)
                .OrderBy(x => x.Name)
                .To<ClassAtListViewModel>()
                .ToList();

            return classes;
        }


        // Get a class by Id
        public T GetById<T>(int id)
        {
            var wantedClass = this.classesRepository.AllAsNoTracking()
                .Where(m => m.Id == id)
                .To<T>()
                .FirstOrDefault();

            return wantedClass;
        }

        // Get classes' count in some school
        public int GetCountBySchoolId(int schoolId)
        {
            return this.classesRepository.AllAsNoTracking()
                .Where(x => x.SchoolId == schoolId)
                .Count();
        }
    }
}
