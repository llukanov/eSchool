namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;

    using ESchool.Web.ViewModels.Material;

    public interface IMaterialsService
    {
        IEnumerable<MaterialAtListViewModel> GetAllInSubject<T>(int subjectId, int page, int itemsPerPage = 20);

        T GetById<T>(string id);

        int GetCountInSubject(int subjectId);
    }
}
