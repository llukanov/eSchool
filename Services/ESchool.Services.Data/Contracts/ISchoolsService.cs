namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Home;
    using ESchool.Web.ViewModels.School;

    public interface ISchoolsService
    {
        Task CreateAsync(CreateSchoolInputModel input);

        Task UpdateAsync(EditSchoolInputModel input, int id);

        Task DeleteAsync(int id);

        Task AddUserToSchool(ApplicationUser user, int schoolId);

        IEnumerable<SchoolAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        T GetById<T>(int id);

        int GetCount();

        School GetSchoolByUserId(string userId);

        string GetAdminName(int schoolId);

        int GetTeachersCount(int schoolId);

        int GetStudentsCount(int schoolId);
    }
}
