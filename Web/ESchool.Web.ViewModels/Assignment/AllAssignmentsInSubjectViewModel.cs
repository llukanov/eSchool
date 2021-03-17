namespace ESchool.Web.ViewModels.Assignment
{
    using System.Collections.Generic;

    using ESchool.Web.ViewModels.Subject;

    public class AllAssignmentsInSubjectViewModel : PagingViewModel
    {
        public int SubjectId { get; set; }

        public SubjectAtListViewModel Subject { get; set; }

        public IEnumerable<AssignmentAtListViewModel> Assignments { get; set; }
    }
}
