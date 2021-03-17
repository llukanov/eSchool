namespace ESchool.Web.ViewModels.Subject
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSubjectInputModel
    {
        [Required(ErrorMessage = "Полето „Наименование“ е задължително!")]
        [MinLength(2, ErrorMessage = "Дължината на „Наименование“ трябва да бъде поне 2 символа.")]
        public string Name { get; set; }
    }
}
