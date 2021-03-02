namespace ESchool.Web.ViewModels.Subject
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SubjectAtListViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClassId { get; set; }

        public ClassInSchool Class { get; set; }
    }
}
