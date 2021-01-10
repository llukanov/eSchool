namespace ESchool.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
