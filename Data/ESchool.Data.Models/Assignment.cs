﻿using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class Assignment : BaseDeletableModel<string>
    {
        public Assignment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        public virtual DateTime Deadline { get; set; }
    }
}
