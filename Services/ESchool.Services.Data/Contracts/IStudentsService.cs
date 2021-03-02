namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.Student;

    public interface IStudentsService
    {
        Task UpdateAsync(EditStudentViewModel input, string userId);

        Task ApproveStudentAsync(string id);

        Task RejectStudentAsync(string id);

        Task AddStudentToClass(string userId, int classId);

        IEnumerable<StudentAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        IEnumerable<StudentAtListViewModel> GetAllStudentsInSchool<T>(int schoolId, int page, int itemsPerPage = 20);

        IEnumerable<StudentAtListViewModel> GetAllStudentsInSchoolForAddingInClass<T>(int schoolId, int page, int itemsPerPage = 20);

        IEnumerable<StudentAtListViewModel> GetAllStudentsInClass<T>(int schoolId, int classInSchoolId, int page, int itemsPerPage = 20);

        T GetById<T>(string id);

        int GetCount();

        int GetCountBySchoolId(int schoolId);
    }
}
