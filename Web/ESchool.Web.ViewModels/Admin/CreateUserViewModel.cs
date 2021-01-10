namespace ESchool.Web.ViewModels.Admin
{
    using System.Collections.Generic;

    using ESchool.Data.Models;

    public class CreateUserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ApplicationRole> Roles { get; set; }
    }
}
