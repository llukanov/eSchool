namespace ESchool.Web.Controllers
{
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Class;
    using Microsoft.AspNetCore.Mvc;

    public class ClassController : Controller
    {
        private readonly ISubjectsService subjectsService;

        public IActionResult Create()
        {
            var viewModel = new CreateClassInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateClassInputModel input)
        {
            // Validation
            if (this.ModelState.IsValid)
            {
                return this.View(input);
            }
            else
            {
                // todo: create class using a service
                // TODO: redirect to class info page
                return this.Redirect("/");
            }
        }
    }
}
