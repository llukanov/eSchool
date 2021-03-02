namespace ESchool.Web.ViewModels.Teacher
{
    using System.Collections.Generic;

    public class AllTeachersInSchoolViewModel : PagingViewModel
    {
        public IEnumerable<TeacherAtListViewModel> Teachers { get; set; }
    }
}
