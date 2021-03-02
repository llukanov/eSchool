namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using ESchool.Common;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.rolesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .Where(x => x.Name != GlobalConstants.SuperAdminRoleName)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
