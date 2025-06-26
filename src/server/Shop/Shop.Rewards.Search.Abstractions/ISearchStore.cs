
namespace Shop.Rewards.Search.Abstractions
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchStore
    {
        Task<T> ExecuteSearchQuery<T>(string procedureName, Dictionary<string, object> parameters, Func<SqlDataReader, Task<T>> mappreFunc);
    }
}
