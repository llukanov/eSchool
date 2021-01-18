namespace ESchool.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using ESchool.Data.Common.Models;

    public class SubjectStudent : BaseModel<int>
    {
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }
    }
}
