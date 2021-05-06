namespace ESchool.Web.ViewModels.Grade
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class GradesOfStudentAtListViewModel : IMapFrom<StudentGrade>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }

        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        public string FullName => this.Student.FirstName + " " + this.Student.LastName;
    }
}
