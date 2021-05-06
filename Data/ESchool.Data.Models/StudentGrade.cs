namespace ESchool.Data.Models
{
    using System;

    using ESchool.Data.Common.Models;

    public class StudentGrade : BaseDeletableModel<string>
    {
        public StudentGrade()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }

        public string TeachertId { get; set; }

        public ApplicationUser Teacher { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
