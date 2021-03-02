namespace ESchool.Web.ViewModels.Student
{
    using System.Collections.Generic;

    public class AllStudentsInSchoolViewModel : PagingViewModel
    {
        public int SchoolId { get; set; }

        public IEnumerable<StudentAtListViewModel> Students { get; set; }
    }
}
