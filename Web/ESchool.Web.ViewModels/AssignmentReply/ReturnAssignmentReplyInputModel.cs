namespace ESchool.Web.ViewModels.AssignmentReply
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.Material;
    using ESchool.Web.ViewModels.Student;

    public class ReturnAssignmentReplyInputModel : IMapFrom<AssignmentReply>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string AssignmentId { get; set; }

        public AssignmentAtListViewModel Assignment { get; set; }

        public ICollection<MaterialAtListViewModel> Files { get; set; }

        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }

        //public string StudentFullName => this.Student.FirstName + " " + this.Student.SecondName + " " + this.Student.LastName;

        [Required]
        public string TeacherReview { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Grades { get; set; }

        [Required]
        public int GradeId { get; set; }

        public Grade Grade { get; set; }
    }
}
