namespace ESchool.Web.ViewModels.School
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SchoolAtListViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Settlement { get; set; }

        public string Province { get; set; }

        public string AdminName { get; set; }

        public int TeachersCount { get; set; }

        public int StudentsCount { get; set; }
    }
}
