namespace ESchool.Web.ViewModels.Class
{
    using System.ComponentModel.DataAnnotations;

    public class CreateClassInputModel
    {
        [Required(ErrorMessage = "Полето „Наименование“ е задължително!")]
        public string Name { get; set; }

        [Required]
        public int SchoolId { get; set; }
    }
}
