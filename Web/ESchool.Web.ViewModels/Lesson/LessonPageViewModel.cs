namespace ESchool.Web.ViewModels.Lesson
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.Material;
    using ESchool.Web.ViewModels.Quiz;

    public class LessonPageViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<MaterialAtListViewModel> Materials { get; set; }

        public IEnumerable<AssignmentAtListViewModel> Assignments { get; set; }

        public IEnumerable<QuizAtListViewModel> Quizzes { get; set; }
    }
}
