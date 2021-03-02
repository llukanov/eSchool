namespace ESchool.Web.ViewModels.Class
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ClassAtListViewModel : IMapFrom<ClassInSchool>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
