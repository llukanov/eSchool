namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ESchool.Web.ViewModels.Admin;

    public interface IAdminsService
    {
        Task ApproveAdminAsync(string id);

        Task RejectAdminAsync(string id);

        IEnumerable<AdminAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        T GetById<T>(string id);

        int GetCount();
    }
}
