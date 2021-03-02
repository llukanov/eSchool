namespace ESchool.Web.ViewModels.School
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EditSchoolInputModel : IMapFrom<School>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Settlement { get; set; }

        [Required]
        public string Province { get; set; }
    }
}
