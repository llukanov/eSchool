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

        IEnumerable<ClassAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        ClassInSchool GetClassOfStudent<T>(ApplicationUser student);

        IEnumerable<ClassAtListViewModel> GetAllClassesInSchool<T>(int schoolId);

        IEnumerable<KeyValuePair<string, string>> GetAllClassesInSchoolAsKeyValuePairs(int schoolId);

        T GetById<T>(int id);

        int GetCount();

        int GetCountBySchoolId(int schoolId);
    }
}
