namespace ESchool.Web.ViewModels.AssignmentReply
{
    using System.Collections.Generic;

    public class AllAssignmentsRepliesOfStudentViewModel : PagingViewModel
    {
        public string StudentId { get; set; }

        public IEnumerable<AssignmentReplyAtListViewModel> AssignmentsReplies { get; set; }
    }
}
