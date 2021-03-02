namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ESchool.Web.ViewModels.Teacher;

    public interface ITeachersService
    {
        Task ApproveTeacherAsync(string id);

        Task RejectTeacherAsync(string id);

        IEnumerable<TeacherAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        IEnumerable<TeacherAtListViewModel> GetAllTeachersInSchool<T>(int schoolId, int page, int itemsPerPage = 20);

        T GetById<T>(string id);

        int GetCount();

        int GetCountBySchoolId(int schoolId);
    }
}
