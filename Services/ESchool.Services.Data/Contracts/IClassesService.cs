namespace ESchool.Services.Data.Contracts
{
    using ESchool.Web.ViewModels.Home;

    using System.Collections.Generic;

    public interface IClassesService
    {
        IndexViewModel GetCount();
    }
}
