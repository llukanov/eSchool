using ESchool.Common;
using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESchool.Services.Data
{
    public class GradesService : IGradesService
    {
        private readonly IDeletableEntityRepository<Grade> gradeRepository;

        public GradesService(
            IDeletableEntityRepository<Grade> gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.gradeRepository
                .AllAsNoTracking()
                .Where(x => x.Id != GlobalConstants.DefaultGradeId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Id)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
