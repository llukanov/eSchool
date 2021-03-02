using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Data.Seeding
{
    internal class SchoolsSeeder : ISeeder
    {
        // Create default schools
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Schools.Any())
            {
                var schools = new List<School>
                {
                    new School { Name = "SuperAdminSchool", Settlement = "Бяла Слатина", Province = "Враца" },
                    new School { Name = "Средно училище \"Васил Левски\"", Settlement = "Бяла Слатина", Province = "Враца" },
                    new School { Name = "ППМГ \"Акад. Иван Ценов\"", Settlement = "Враца", Province = "Враца" },
                    new School { Name = "7.СУ \"Свети Седмочисленици\"", Settlement = "София", Province = "София" },
                    new School { Name = "26.СУ \"Йордан Йовков\"", Settlement = "София", Province = "София" },
                    new School { Name = "27.СУ \"Акад. Георги Караславов\"", Settlement = "София", Province = "София" },
                };

                dbContext.Schools.AddRange(schools);
            }
        }
    }
}
