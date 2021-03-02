namespace ESchool.Web.ViewModels.Admin
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AdminAtListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public string FullName => this.FirstName + " " + this.SecondName + " " + this.LastName;

        public string SchoolId { get; set; }

        public School School { get; set; }
    }
}
