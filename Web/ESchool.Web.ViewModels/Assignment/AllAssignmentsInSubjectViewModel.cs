using ESchool.Web.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Assignment
{
    public class AllAssignmentsInSubjectViewModel : PagingViewModel
    {
        public int SubjectId { get; set; }

        public SubjectAtListViewModel Subject { get; set; }

        public IEnumerable<AssignmentAtListViewModel> Assignments { get; set; }
    }
}
