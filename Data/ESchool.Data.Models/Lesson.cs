namespace ESchool.Data.Models
{
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class Lesson : BaseDeletableModel<int>
    {
        public Lesson()
        {
            this.Materials = new HashSet<Material>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public string ChatId { get; set; }

        public Chat Chat { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
