namespace ESchool.Data.Models
{
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class School : BaseDeletableModel<int>
    {
        public School()
        {
            this.Classes = new HashSet<Class>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
