using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using ESchool.Web.ViewModels.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESchool.Services.Data
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IDeletableEntityRepository<Material> materialRepository;

        public MaterialsService(
            IDeletableEntityRepository<Material> materialRepository)
        {
            this.materialRepository = materialRepository;
        }

        public IEnumerable<MaterialAtListViewModel> GetAllInSubject<T>(int subjectId, int page, int itemsPerPage = 20)
        {
            var materials = this.materialRepository
                .AllAsNoTracking()
                .Where(x => x.Lesson.SubjectId == subjectId)
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<MaterialAtListViewModel>()
                .ToList();

            return materials;
        }

        // Get specific material by its id
        public T GetById<T>(string id)
        {
            var material = this.materialRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return material;
        }

        public int GetCountInSubject(int subjectId)
        {
            return this.materialRepository
                .AllAsNoTracking()
                .Where(x => x.Lesson.SubjectId == subjectId)
                .Count();
        }
    }
}
