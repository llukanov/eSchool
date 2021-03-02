using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class AssignmentReply : BaseDeletableModel<string>
    {
        public AssignmentReply()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string AssignmentId { get; set; }

        public Assignment Assignment { get; set; }

        public virtual ICollection<Material> Files { get; set; }

        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }

        public virtual string TeacherReview { get; set; }

        public virtual int GradeId { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
