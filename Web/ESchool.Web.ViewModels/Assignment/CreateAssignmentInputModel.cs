using ESchool.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESchool.Web.ViewModels.Assignment
{
    public class CreateAssignmentInputModel
    {
        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        public int LessonId { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> Materials { get; set; }

        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        [Required]
        public DateTime Deadline { get; set; }
    }
}
