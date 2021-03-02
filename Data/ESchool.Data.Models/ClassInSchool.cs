namespace ESchool.Data.Models
{
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class ClassInSchool : BaseDeletableModel<int>
    {
        public ClassInSchool()
        {
            this.Subjects = new HashSet<Subject>();
            this.Students = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }

        public int SchoolId { get; set; }

        public virtual School School { get; set; }
    }
}
