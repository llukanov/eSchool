using ESchool.Web.ViewModels.Lesson;
using ESchool.Web.ViewModels.Material;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface ILessonsService
    {
        Task CreateAsync(CreateLessonInputModel input, string materialPath);

        Task UpdateAsync(EditLessonInputModel input, int id);

        IEnumerable<LessonAtListViewModel> GetAll<T>(int page, int itemsPerPage = 20);

        T GetById<T>(int id);
    }
}
