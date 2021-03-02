namespace ESchool.Web.ViewModels.Lesson
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CreateLessonInputModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        public int SubjectId { get; set; }

        [BindProperty]
        public IEnumerable<IFormFile> Materials { get; set; }

        public string TeacherId { get; set; }
    }
}
