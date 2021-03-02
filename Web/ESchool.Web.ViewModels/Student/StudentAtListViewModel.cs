namespace ESchool.Web.ViewModels.Student
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentAtListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public int ClassInSchoolId { get; set; }

        public ClassInSchool ClassInSchool { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        public string FullName => this.FirstName + " " + this.SecondName + " " + this.LastName;
    }
}
