
namespace Shop.Rewards.Store
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class Store : IStore
    {
        private readonly static int DefaultPageSize = 10;
        private readonly string connectionString;

        public Store(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task CreateNewPurchase(PurchaseRecord purchase)
        {
            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_CreateNewPurchase", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StoreName", purchase.StoreName);
            cmd.Parameters.AddWithValue("@Category", purchase.Category.ToString());
            cmd.Parameters.AddWithValue("@Amount", purchase.Amount);
            cmd.Parameters.AddWithValue("@UserId", purchase.UserId);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<PurchaseRecordCollection> GetAllPurchases(int pageNumber)
        {
            var purchases = new List<PurchaseRecord>();
            int totalCount = 0;

            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_GetAllPurchases", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", DefaultPageSize);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                purchases.Add(this.ReadPurchaseRecordFromReader(reader));
            }

            if (await reader.NextResultAsync())
            {
                if (await reader.ReadAsync())
                {
                    totalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));
                }
            }

            return new PurchaseRecordCollection(purchases, totalCount, ++pageNumber);
        }

        public async Task<PurchaseRecord> GetPurchaseById(Guid id)
        {
            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_GetPurchaseById", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
                return null;

            await reader.ReadAsync();

            return this.ReadPurchaseRecordFromReader(reader);
        }

        public async Task<bool> DeletePurchase(Guid id)
        {
            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_DeletePurchase", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdatePurchase(Guid id, PurchaseRecord purchase)
        {
            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_UpdatePurchase", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", purchase.Id);
            cmd.Parameters.AddWithValue("@StoreName", purchase.StoreName);
            cmd.Parameters.AddWithValue("@Category", purchase.Category.ToString());
            cmd.Parameters.AddWithValue("@Amount", purchase.Amount);

            await conn.OpenAsync();
            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        private PurchaseRecord ReadPurchaseRecordFromReader(SqlDataReader reader)
        {
            return new PurchaseRecord(reader.GetGuid(reader.GetOrdinal("Id")), reader.GetGuid(reader.GetOrdinal("UserId")), reader.GetString(reader.GetOrdinal("StoreName")),
                         Enum.Parse<Category>(reader.GetString(reader.GetOrdinal("Category"))), reader.GetDecimal(reader.GetOrdinal("Amount")), reader.GetDateTime(reader.GetOrdinal("CreatedAt")));
        }
    }
}
