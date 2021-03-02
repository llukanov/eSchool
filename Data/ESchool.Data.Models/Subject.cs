namespace ESchool.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ESchool.Data.Common.Models;

    public class Subject : BaseDeletableModel<int>
    {
        public Subject()
        {
            this.Students = new HashSet<SubjectStudent>();
            this.Lessons = new HashSet<Lesson>();
        }

        public string Name { get; set; }

        public int ClassId { get; set; }

        public ClassInSchool Class { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        public virtual ICollection<SubjectStudent> Students { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
