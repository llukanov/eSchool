using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Services.Data.Contracts
{
    public interface IGradesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
