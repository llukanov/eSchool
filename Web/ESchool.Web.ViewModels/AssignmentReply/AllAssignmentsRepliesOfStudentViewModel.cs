using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.AssignmentReply
{
    public class AllAssignmentsRepliesOfStudentViewModel : PagingViewModel
    {
        public string StudentId { get; set; }

        public IEnumerable<AssignmentReplyAtListViewModel> AssignmentsReplies { get; set; }
    }
}
