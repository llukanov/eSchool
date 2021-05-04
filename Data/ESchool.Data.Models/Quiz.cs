namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ESchool.Data.Common.Models;

    public class Quiz : BaseDeletableModel<string>
    {
        public Quiz()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public bool IsActivated { get; set; }
    }
}
