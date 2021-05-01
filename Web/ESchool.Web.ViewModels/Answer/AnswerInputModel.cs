namespace ESchool.Web.ViewModels.Answer
{
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AnswerInputModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        //public string SanitizedText => new HtmlSanitizer().Sanitize(this.Text);

        public bool IsRightAnswer { get; set; }

        public string QuestionId { get; set; }
    }
}
