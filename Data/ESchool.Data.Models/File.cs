namespace ESchool.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using ESchool.Data.Common.Models;

    public class File : BaseDeletableModel<string>
    {
        public File()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Exstension { get; set; }
    }
}
