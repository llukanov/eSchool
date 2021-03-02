using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Subject
{
    public class AllSubjectsViewModel
    {
        public IEnumerable<SubjectAtListViewModel> Subjects { get; set; }
    }
}
