namespace ESchool.Web.ViewModels.Lesson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class EditLessonInputModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето „Тема“ е задължително!")]
        [MinLength(4, ErrorMessage = "Дължината на „Тема“ трябва да бъде поне 4 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето „Инструкции“ е задължително и трябва да съдържа поне 15 символа!")]
        [MinLength(15, ErrorMessage = "Дължината на „Инструкции“ трябва да бъде поне 15 символа.")]
        public string Description { get; set; }

        public int SubjectId { get; set; }

        public string TeacherId { get; set; }
    }
}
