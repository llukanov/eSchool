namespace ESchool.Web.ViewModels.Class
{
    using System.ComponentModel.DataAnnotations;

    public class CreateClassInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int SchoolId { get; set; }
    }
}
