using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Services.Data.Contracts
{
    public interface ISubjectsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
