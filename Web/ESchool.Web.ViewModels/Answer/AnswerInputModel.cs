namespace ESchool.Web.ViewModels.Answer
{
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AnswerInputModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" е задължително!")]
        [StringLength(
           1000,
           ErrorMessage = "Полето \"{0}\" трябва да бъде с дължина поне {2} и максимум {1} символа.",
           MinimumLength = 5)]
        [Display(Name = "Отговор")]
        public string Text { get; set; }

        //public string SanitizedText => new HtmlSanitizer().Sanitize(this.Text);

        public bool IsRightAnswer { get; set; }

        public string QuestionId { get; set; }
    }
}
