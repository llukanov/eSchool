namespace ESchool.Web.ViewModels.Class
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Student;

    public class ClassPageViewModel : IMapFrom<ClassInSchool>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }

        public IEnumerable<StudentAtListViewModel> Students { get; set; }

        public IEnumerable<StudentAtListViewModel> StudentsInSchool { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }
    }
}
