namespace ESchool.Web.ViewModels.Lesson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class LessonAtListViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreatedOn { get; set; }

        public DateTime LocalCreatedOn => this.CreatedOn.AddHours(2.00);
    }
}
