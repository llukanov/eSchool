namespace ESchool.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.Grade;

    public interface IStudentGradesServices
    {
        Task AddGrade(int subjectId, string studentId, string teacherId, int gradeId);

        IEnumerable<StudentGradeAtListViewModel> GetAllInSubject<T>(int subjectId);

        IEnumerable<T> GetAllOfStudents<T>(string studentId);
    }
}
