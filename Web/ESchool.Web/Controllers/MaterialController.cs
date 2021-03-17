namespace ESchool.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.Pkcs;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Material;
    using ESchool.Web.ViewModels.Subject;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MaterialController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Material> materialRepository;
        private readonly IMaterialsService materialsService;
        private readonly ISubjectsService subjectsService;
        private readonly IWebHostEnvironment environment;

        public MaterialController(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Material> materialRepository,
            IMaterialsService materialsService,
            ISubjectsService subjectsService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.materialRepository = materialRepository;
            this.materialsService = materialsService;
            this.subjectsService = subjectsService;
            this.environment = environment;
        }

        // Mime Types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.ms-word" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.ms-excel" },
                { ".ppt", "application/vnd.ms-powerpoint" },
                { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
            };
        }

        // Get all materials in some subject
        [Authorize(Roles = GlobalConstants.TeacherRoleName + "," + GlobalConstants.StudentRoleName)]
        public IActionResult AllInSubject(int subjectId, int id = 1)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this.userManager.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (!user.IsApproved)
            {
                return this.View("UnApproved");
            }

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 10;
            var viewModel = new AllMaterialsInSubjectViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageIndex = id,
                ItemsCount = this.materialsService.GetCountInSubject(subjectId),
                SubjectId = subjectId,
                Subject = this.subjectsService.GetById<SubjectAtListViewModel>(subjectId),
                Materials = this.materialsService.GetAllInSubject<MaterialAtListViewModel>(subjectId, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        // Download files
        public async Task<IActionResult> Download(string materialId, string extension)
        {
            // Search file in database
            var material = this.materialsService.GetById<MaterialAtListViewModel>(materialId);

            // Search file in wwwroot
            var materialName = materialId + extension;

            if (materialName == null)
            {
                return this.Content("Файлът не съществува!");
            }

            var filePath = Path.Combine(
                           $"{this.environment.WebRootPath}/materials/materials/",
                           materialName);

            // Return file
            var memory = new MemoryStream();

            var stream = new FileStream(filePath, FileMode.Open);
            await stream.CopyToAsync(memory);

            memory.Position = 0;

            return this.File(memory, this.GetContentType(filePath), material.Name + extension);
        }

        // Get Mime content type
        private string GetContentType(string path)
        {
            var types = this.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
    }
}
