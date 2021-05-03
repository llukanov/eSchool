namespace ESchool.Web.ViewModels.Answer
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AnswerAtListViewModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsRightAnswer { get; set; }
    }
}
