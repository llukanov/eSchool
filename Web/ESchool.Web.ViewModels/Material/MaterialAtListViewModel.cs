namespace ESchool.Web.ViewModels.Material
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Lesson;

    public class MaterialAtListViewModel : IMapFrom<Material>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LessonAtListViewModel Lesson { get; set; }

        public string Extension { get; set; }
    }
}
