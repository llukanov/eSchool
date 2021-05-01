namespace ESchool.Web.ViewModels.Quiz
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class QuizAtListViewModel : IMapFrom<Quiz>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }
    }
}
