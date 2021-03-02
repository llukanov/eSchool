namespace ESchool.Web.ViewModels.Assignment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AssignmentPageViewModel : IMapFrom<Assignment>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public Lesson Lesson { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ApplicationUser Teacher { get; set; }

        public DateTime Deadline { get; set; }

        public string StudentReplyText { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> StudentReplyFiles { get; set; }
    }
}
