namespace ESchool.Web.ViewModels.Home
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AdminIndexViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public bool IsApproved { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        public string SchoolName { get; set; }

        public int TeachersCount { get; set; }

        public int StudentsCount { get; set; }

        public int ClassesCount { get; set; }
    }
}
