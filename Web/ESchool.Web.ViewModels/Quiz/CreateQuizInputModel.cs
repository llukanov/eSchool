namespace ESchool.Web.ViewModels.Quiz
{
    using System.ComponentModel.DataAnnotations;

    public class CreateQuizInputModel
    {
        [Required]
        [StringLength(
            250,
            ErrorMessage = "{0} трябва да бъде с дължина поне {2} и максимум {1} символа.",
            MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string CreatorId { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public int LessonId { get; set; }
    }
}
