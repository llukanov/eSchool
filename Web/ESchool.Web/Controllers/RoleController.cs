namespace ESchool.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = this.roleManager.Roles.ToList();
            return this.View(roles);
        }

        public IActionResult Create()
        {
            return this.View(new ApplicationRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            await this.roleManager.CreateAsync(role);
            return this.RedirectToAction("Index");
        }
    }
}
