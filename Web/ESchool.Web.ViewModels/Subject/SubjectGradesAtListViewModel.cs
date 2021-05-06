namespace ESchool.Web.ViewModels.Subject
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Grade;

    public class SubjectGradesAtListViewModel : IMapFrom<Subject>
    {

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
