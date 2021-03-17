namespace ESchool.Web.ViewModels.AssignmentReply
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Web.Mvc;
    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class SendAssignmentReplyInputModel
    {
        public string AssignmentId { get; set; }

        public string Description { get; set; }

        public Lesson Lesson { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ApplicationUser Teacher { get; set; }

        public string StudentId { get; set; }

        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Полето „Отговор“ е задължително и трябва да съдържа поне 100 символа!")]
        [AllowHtml]
        public string StudentReplyText { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> StudentReplyFiles { get; set; }
    }
}
