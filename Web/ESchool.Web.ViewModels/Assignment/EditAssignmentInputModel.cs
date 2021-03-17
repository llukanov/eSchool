namespace ESchool.Web.ViewModels.Assignment
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ESchool.Data.Models;

    using ESchool.Services.Mapping;

    public class EditAssignmentInputModel : IMapFrom<Assignment>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Полето „Инструкции“ е задължително и трябва да съдържа поне 15 символа!")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето „Краен срок“ е задължително!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Deadline { get; set; }
    }
}
