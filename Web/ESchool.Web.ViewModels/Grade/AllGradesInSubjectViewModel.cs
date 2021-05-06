namespace ESchool.Web.ViewModels.Grade
{
    using System.Collections.Generic;

    using ESchool.Web.ViewModels.Subject;

    public class AllGradesInSubjectViewModel
    {
        public int SubjectId { get; set; }

        public SubjectAtListViewModel Subject { get; set; }

        public IEnumerable<StudentGradeAtListViewModel> Students { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Grades { get; set; }
    }
}
