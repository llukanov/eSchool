namespace ESchool.Web.ViewModels.Assignment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AssignmentAtListViewModel : IMapFrom<Assignment>
    {
        public string Id { get; set; }

        public Lesson Lesson { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Deadline { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }
    }
}
