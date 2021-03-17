namespace ESchool.Web.ViewModels.School
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSchoolInputModel
    {
        [Required(ErrorMessage = "Полето „Наименование“ е задължително!")]
        [MinLength(4, ErrorMessage = "Дължината на „Наименование“ трябва да бъде поне 4 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето „Населено място“ е задължително!")]
        [MinLength(3, ErrorMessage = "Дължината на „Населено място“ трябва да бъде поне 3 символа.")]
        public string Settlement { get; set; }

        [Required(ErrorMessage = "Полето „Област“ е задължително!")]
        [MinLength(4, ErrorMessage = "Дължината на „Област“ трябва да бъде поне 4 символа.")]
        public string Province { get; set; }
    }
}
