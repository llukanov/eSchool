namespace ESchool.Web.ViewModels.Subject
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;

    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Lesson;

    public class SubjectPageViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        // Using for Subject Navbar
        public int SubjectId => this.Id;

        public string Name { get; set; }

        public int ClassId { get; set; }

        public ClassInSchool Class { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        public IEnumerable<SubjectStudent> Students { get; set; }

        public IEnumerable<LessonAtListViewModel> Lessons { get; set; }
    }
}
