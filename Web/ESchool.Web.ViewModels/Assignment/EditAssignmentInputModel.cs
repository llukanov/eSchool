namespace ESchool.Web.ViewModels.Assignment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;

    using ESchool.Services.Mapping;

    public class EditAssignmentInputModel : IMapFrom<Assignment>
    {
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Deadline { get; set; }
    }
}
