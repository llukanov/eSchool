using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESchool.Web.ViewModels.Subject
{
    public class CreateSubjectInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
