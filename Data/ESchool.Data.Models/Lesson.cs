namespace ESchool.Data.Models
{
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class Lesson : BaseDeletableModel<int>
    {
        public Lesson()
        {
            this.Files = new HashSet<File>();
        }

        public string Name { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
