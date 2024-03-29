﻿namespace ESchool.Web.ViewModels.Assignment 
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;

    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.AssignmentReply;
    using ESchool.Web.ViewModels.Lesson;

    public class AssignmentPageForTeacherViewModel : IMapFrom<Assignment>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public LessonAtListViewModel Lesson { get; set; }

        public ICollection<Material> Materials { get; set; }

        public ApplicationUser Teacher { get; set; }

        public DateTime Deadline { get; set; }

        public IEnumerable<AssignmentReplyAtListViewModel> AssignmentReplies { get; set; }

        public int SubjectId => this.Lesson.SubjectId;
    }
}
