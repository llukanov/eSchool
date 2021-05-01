using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
