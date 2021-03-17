namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Class;

    public interface IClassesService
    {
        Task CreateAsync(CreateClassInputModel input, int schoolId);

        Task UpdateAsync(EditClassInputModel input, int id);

        Task DeleteAsync(int id);

        ClassInSchool GetClassOfStudent<T>(ApplicationUser student);

        IEnumerable<ClassAtListViewModel> GetAllClassesInSchool<T>(int schoolId);

        T GetById<T>(int id);

        int GetCountBySchoolId(int schoolId);
    }
}
