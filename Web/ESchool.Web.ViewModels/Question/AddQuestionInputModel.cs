namespace ESchool.Web.ViewModels.Question
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Answer;
    using ESchool.Web.ViewModels.Quiz;

    public class AddQuestionInputModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        [Required]
        //[StringLength(
        //   5,
        //   ErrorMessage = "Повече символи",
        //   MinimumLength = 1000)]
        public string Text { get; set; }

        public IEnumerable<AnswerInputModel> Answers { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string QuizId { get; set; }

        public QuizPageViewModel Quiz { get; set; }
    }
}
