namespace ESchool.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using ESchool.Data;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels;
    using ESchool.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IClassesService classesService;

        public HomeController(IClassesService classesService)
        {
            this.classesService = classesService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var viewModel = this.classesService.GetCount();

                return this.View("AdministratorIndex", viewModel);
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
