namespace ESchool.Web.Controllers
{
    using System.Linq;

    using ESchool.Data;
    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Admin;
    using Microsoft.AspNetCore.Mvc;

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult CreateUser()
        {
            var model = new CreateUserViewModel();
            ApplicationDbContext appDbContext = new ApplicationDbContext();

            var rolesFromDb = appDbContext.Roles.ToList();

            model.Roles = rolesFromDb.Select(r => new ApplicationRole { Id = r.Id, Name = r.Name }).ToList();

            return this.View();
        }
    }
}
