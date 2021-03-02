namespace ESchool.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using ESchool.Data.Common.Models;

    public class Material : BaseDeletableModel<string>
    {
        public Material()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public string AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public string AssignmentReplyId { get; set; }

        public virtual AssignmentReply AssignmentReply { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extension { get; set; }
    }
}
