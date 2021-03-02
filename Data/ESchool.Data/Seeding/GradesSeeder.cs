using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Data.Seeding
{
    public class GradesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Grades.Any())
            {
                var grades = new List<Grade>
                {
                    new Grade { Name = "Не е оценена" },
                    new Grade { Name = "Не е предадена" },
                    new Grade { Name = "Слаб (2)" },
                    new Grade { Name = "Среден (3)" },
                    new Grade { Name = "Добър (4)" },
                    new Grade { Name = "Много добър (5)" },
                    new Grade { Name = "Отличен (6)" },
                };

                dbContext.Grades.AddRange(grades);
            }
        }
    }
}
