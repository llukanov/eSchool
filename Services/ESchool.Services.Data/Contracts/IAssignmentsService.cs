using ESchool.Web.ViewModels.Assignment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IAssignmentsService
    {
        Task CreateAsync(CreateAssignmentInputModel input, string materialPath);

        Task UpdateAsync(EditAssignmentInputModel input, string assignmentId);

        IEnumerable<CreateAssignmentInputModel> GetAll<T>(int page, int itemsPerPage = 20);

        IEnumerable<AssignmentAtListViewModel> GetAllAssignmensInLesson<T>(int lessonId);

        IEnumerable<AssignmentAtListViewModel> GetAllInSubject<T>(int subjectId, int page, int itemsPerPage = 20);

        T GetById<T>(string assignmentId);

        int GetCountInSubject(int subjectId);
    }
}
