﻿namespace ESchool.Web.ViewModels.Quiz
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Question;

    public class QuizPageViewModel : IMapFrom<Quiz>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<QuestionAtListViewModel> Questions { get; set; }

        public int TotalScores { get; set; }

        public bool IsActivated { get; set; }
    }
}
