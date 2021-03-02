using ESchool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Data.Models
{
    public class Grade : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
