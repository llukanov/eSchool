namespace ESchool.Web.Areas.Administration.Controllers
{
    using ESchool.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.SuperAdminRoleName)]
    public class SuperAdminController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
