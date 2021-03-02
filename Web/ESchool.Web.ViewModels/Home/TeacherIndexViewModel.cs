namespace ESchool.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Subject;

    public class TeacherIndexViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public bool IsApproved { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        public string SchoolName { get; set; }

        public int ClassesCount { get; set; }

        public IEnumerable<SubjectAtListViewModel> Subjects { get; set; }
    }
}
