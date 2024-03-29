﻿namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.AssignmentReply;

    public interface IAssignmentRepliesService
    {
        Task SendAsync(SendAssignmentReplyInputModel input, string materialPath);

        Task UpdateAsync(ReturnAssignmentReplyInputModel input, string assignmentReplyId, string teacherId);

        IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfAssignment<T>(string assignmentId);

        IEnumerable<AssignmentReplyAtListViewModel> GetAllRepliesOfStudent<T>(string studentId, int page, int itemsPerPage = 20);

        T GetById<T>(string assignmentReplyId);

        int GetCountRepliesOfStudent(string studentId);
    }
}
