namespace ESchool.Web.ViewModels.Assignment
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CreateAssignmentInputModel
    {
        [Required(ErrorMessage = "Полето „Инструкции“ е задължително и трябва да съдържа поне 15 символа!")]
        [AllowHtml]
        public string Description { get; set; }

        public int LessonId { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> Materials { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        [Required(ErrorMessage = "Полето „Краен срок“ е задължително!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Deadline { get; set; }
    }
}
