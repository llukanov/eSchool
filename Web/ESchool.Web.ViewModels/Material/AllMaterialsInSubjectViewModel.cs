namespace ESchool.Web.ViewModels.Material
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Subject;

    public class AllMaterialsInSubjectViewModel : PagingViewModel
    {
        public int SubjectId { get; set; }

        public SubjectAtListViewModel Subject { get; set; }

        public IEnumerable<MaterialAtListViewModel> Materials { get; set; }
    }
}
