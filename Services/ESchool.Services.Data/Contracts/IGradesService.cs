namespace ESchool.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IGradesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
