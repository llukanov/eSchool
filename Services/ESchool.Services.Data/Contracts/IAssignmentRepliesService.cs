using ESchool.Web.ViewModels.AssignmentReply;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IAssignmentRepliesService
    {
        Task SendAsync(SendAssignmentReplyInputModel input, string materialPath);

        Task UpdateAsync(ReturnAssignmentReplyInputModel input, string assignmentReplyId);

        IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfAssignment<T>(string assignmentId);

        IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfStudent<T>(string studentId, int page, int itemsPerPage = 20);

        T GetById<T>(string assignmentReplyId);

        int GetCountRepliesOfStudent(string studentId);
    }
}
