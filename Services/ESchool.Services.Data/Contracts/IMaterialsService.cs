using ESchool.Web.ViewModels.Material;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Services.Data.Contracts
{
    public interface IMaterialsService
    {
        IEnumerable<MaterialAtListViewModel> GetAllInSubject<T>(int subjectId, int page, int itemsPerPage = 20);

        T GetById<T>(string id);

        int GetCountInSubject(int subjectId);
    }
}
