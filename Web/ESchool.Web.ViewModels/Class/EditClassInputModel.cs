namespace ESchool.Web.ViewModels.Class
{
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class EditClassInputModel : IMapFrom<ClassInSchool>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
