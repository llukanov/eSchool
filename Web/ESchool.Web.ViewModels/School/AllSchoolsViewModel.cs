namespace ESchool.Web.ViewModels.School
{
    using System.Collections.Generic;

    public class AllSchoolsViewModel : PagingViewModel
    {
        public IEnumerable<SchoolAtListViewModel> Schools { get; set; }
    }
}
