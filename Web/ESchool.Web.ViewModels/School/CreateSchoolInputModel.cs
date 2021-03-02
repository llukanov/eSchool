namespace ESchool.Web.ViewModels.School
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSchoolInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Settlement { get; set; }

        [Required]
        public string Province { get; set; }
    }
}
