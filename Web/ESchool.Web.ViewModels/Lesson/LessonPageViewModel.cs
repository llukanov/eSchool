namespace ESchool.Web.ViewModels.Lesson
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Assignment;
    using ESchool.Web.ViewModels.Material;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class LessonPageViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<MaterialAtListViewModel> Materials { get; set; }

        public IEnumerable<AssignmentAtListViewModel> Assignments { get; set; }
    }
}
