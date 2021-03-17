namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Web.ViewModels.Lesson;

    public interface ILessonsService
    {
        Task CreateAsync(CreateLessonInputModel input, string materialPath);

        Task UpdateAsync(EditLessonInputModel input, int id);

        T GetById<T>(int id);
    }
}
