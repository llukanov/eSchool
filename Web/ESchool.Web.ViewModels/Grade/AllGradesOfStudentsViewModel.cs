namespace ESchool.Web.ViewModels.Grade
{
    using System.Collections.Generic;

    using ESchool.Web.ViewModels.Student;

    public class AllGradesOfStudentsViewModel
    {
        public StudentAtListViewModel Student { get; set; }

        public IEnumerable<GradesOfStudentAtListViewModel> Grades { get; set; }
    }
}
