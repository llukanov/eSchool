namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IRolesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
