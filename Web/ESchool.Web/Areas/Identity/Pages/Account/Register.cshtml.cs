namespace ESchool.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRolesService rolesService;
        private readonly ISchoolsService schoolsService;
        private readonly IDeletableEntityRepository<School> schoolsRepository;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager,
            IRolesService rolesService,
            ISchoolsService schoolsService,
            IDeletableEntityRepository<School> schoolsRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleManager = roleManager;
            this.rolesService = rolesService;
            this.schoolsService = schoolsService;
            this.schoolsRepository = schoolsRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RolesItems { get; set; }

        // Register Input model
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Име")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Презиме")]
            public string SecondName { get; set; }

            [Required]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0}та трябва да бъде поне {2} и максимум {1} символа дължина.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърждаване на паролата")]
            [Compare("Password", ErrorMessage = "Паролите трябва да съвпадат.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Роля")]
            public string Role { get; set; }

            [Required]
            [Display(Name = "Код на училище")]
            public int SchoolCode { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.RolesItems = this.rolesService.GetAllAsKeyValuePairs();
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            var role = this.roleManager.FindByIdAsync(this.Input.Role).Result;

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var school = this.schoolsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == this.Input.SchoolCode)
                    .FirstOrDefault();

                if (school == null)
                {
                    this.TempData["Message"] = "Невалиден код за присъединяване към училище!";
                    return this.LocalRedirect("/Identity/Account/Register");
                }

                var user = new ApplicationUser {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    SecondName = this.Input.SecondName,
                    LastName = this.Input.LastName,
                    SchoolId = this.Input.SchoolCode,
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    this.logger.LogInformation("Потребителят създаде нов акаунт с парола.");

                    //await this.schoolsService.AddUserToSchool(user, this.Input.SchoolCode);
                    await this.userManager.AddToRoleAsync(user, role.Name);

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            this.RolesItems = this.rolesService.GetAllAsKeyValuePairs();

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
