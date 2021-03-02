namespace ESchool.Web.ViewModels.Class
{
    using System.Collections.Generic;

    public class AllClassesInSchoolViewModel : PagingViewModel
    {
        public IEnumerable<ClassAtListViewModel> Classes { get; set; }
    }
}
