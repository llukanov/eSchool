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

        [Required(ErrorMessage = "Полето \"{0}\" е задължително!")]
        [StringLength(
           1000,
           ErrorMessage = "Полето \"{0}\" трябва да бъде с дължина поне {2} и максимум {1} символа.",
           MinimumLength = 5)]
        [Display(Name = "Въпрос")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Полето {0} е задължително!")]
        [Display(Name = "Точки")]
        public int Scores { get; set; }

        public IEnumerable<AnswerInputModel> Answers { get; set; }

        [Required(ErrorMessage = "Полето е задължително!")]
        public string QuizId { get; set; }

        public QuizPageViewModel Quiz { get; set; }
    }
}
