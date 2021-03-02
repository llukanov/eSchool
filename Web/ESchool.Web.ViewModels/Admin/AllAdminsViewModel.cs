namespace ESchool.Web.ViewModels.Admin
{
    using System.Collections.Generic;

    public class AllAdminsViewModel : PagingViewModel
    {
        public IEnumerable<AdminAtListViewModel> Admins { get; set; }
    }
}
