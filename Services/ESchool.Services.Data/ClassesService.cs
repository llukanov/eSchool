namespace ESchool.Services.Data
{
    using System.Linq;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Home;

    public class ClassesService : IClassesService
    {
        private readonly IDeletableEntityRepository<Class> classesRepository;

        public ClassesService(IDeletableEntityRepository<Class> classesRepository)
        {
            this.classesRepository = classesRepository;
        }



        public IndexViewModel GetCount()
        {
            var data = new IndexViewModel
            {
                ClassesCount = this.classesRepository.All().Count(),
            };
            return data;
        }
    }
}
