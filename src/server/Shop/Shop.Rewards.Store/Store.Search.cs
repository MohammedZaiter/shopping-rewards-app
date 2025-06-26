
namespace Shop.Rewards.Store
{
    using Microsoft.Data.SqlClient;
    using Shop.Rewards.Search.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed partial class Store : ISearchStore
    {
        public async Task<T> ExecuteSearchQuery<T>(string procedureName, Dictionary<string, object> parameters, Func<SqlDataReader, Task<T>> mappreFunc)
        {
            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            return await mappreFunc(reader);
        }
    }
}
