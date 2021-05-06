namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Subject;

    public interface ISubjectsService
    {
        Task CreateAsync(CreateSubjectInputModel input, int classInSchoolId, string teacherId);

        IEnumerable<SubjectAtListViewModel> GetAllSubjectOfTeacher<T>(string teacherId);

        IEnumerable<SubjectAtListViewModel> GetAllSubjectsOfStudent<T>(ApplicationUser student);

        T GetById<T>(int id);

        int GetIdByQuizId(string quizId);
    }
}
