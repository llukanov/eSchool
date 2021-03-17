namespace ESchool.Web.ViewModels.AssignmentReply
{
    using System;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.Student;

    public class AssignmentReplyAtListViewModel : IMapFrom<AssignmentReply>
    {
        public string Id { get; set; }

        public string StudentId { get; set; }

        public StudentAtListViewModel Student { get; set; }

        public AssignmentAtListViewModel Assignment { get; set; }

        public DateTime CreatedOn { get; set; }

        public string TeacherReview { get; set; }

        public Grade Grade { get; set; }

        public string StudentFullName => this.Student.FirstName + " " + this.Student.SecondName + " " + this.Student.LastName;
    }
}
