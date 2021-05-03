namespace ESchool.Web.ViewModels.Question
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SolvedQuestionAtListViewModel : IMapFrom<SolvedQuestion>
    {
        public string Id { get; set; }

        public QuestionAtListViewModel Question { get; set; }

        public string StudentAnswer { get; set; }

        public int Scores { get; set; }
    }
}
