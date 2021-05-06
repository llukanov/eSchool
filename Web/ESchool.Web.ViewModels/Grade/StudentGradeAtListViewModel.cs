namespace ESchool.Web.ViewModels.Grade
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentGradeAtListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.SecondName + " " + this.LastName;

        public IEnumerable<GradesOfStudentAtListViewModel> StudentGrades { get; set; }
    }
}
