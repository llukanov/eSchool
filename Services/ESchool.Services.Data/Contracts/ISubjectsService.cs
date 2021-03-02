using ESchool.Data.Models;
using ESchool.Web.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface ISubjectsService
    {
        Task CreateAsync(CreateSubjectInputModel input, int classInSchoolId, string teacherId);

        IEnumerable<SubjectAtListViewModel> GetAllSubjectOfTeacher<T>(string teacherId);

        IEnumerable<SubjectAtListViewModel> GetAllSubjectsOfStudent<T>(ApplicationUser student);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        T GetById<T>(int id);
    }
}
