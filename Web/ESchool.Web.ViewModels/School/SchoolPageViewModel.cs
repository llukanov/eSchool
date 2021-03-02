namespace ESchool.Web.ViewModels.School
{
    using System;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SchoolPageViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Settlement { get; set; }

        public string Province { get; set; }

        public string AdminName { get; set; }

        public int TeachersCount { get; set; }

        public int StudentsCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
