namespace ESchool.Web.ViewModels.Material
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class MaterialAtListViewModel : IMapFrom<Material>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Lesson Lesson { get; set; }

        public string Extension { get; set; }
    }
}
