namespace ESchool.Data.Models
{
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class School : BaseDeletableModel<int>
    {
        public School()
        {
            this.Classes = new HashSet<ClassInSchool>();
        }

        public string Name { get; set; }

        public string Settlement { get; set; }

        public string Province { get; set; }

        public virtual ICollection<ClassInSchool> Classes { get; set; }
    }
}
