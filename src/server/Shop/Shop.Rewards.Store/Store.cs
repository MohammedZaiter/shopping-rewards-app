
namespace Shop.Rewards.Store
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class Store : IStore
    {
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

        public async Task<IEnumerable<PurchaseRecord>> GetAllPurchases(int pageNumber)
        {
            var purchases = new List<PurchaseRecord>();

            using var conn = new SqlConnection(this.connectionString);
            using var cmd = new SqlCommand("usp_GetAllPurchases", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                purchases.Add(new PurchaseRecord
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    StoreName = reader.GetString(reader.GetOrdinal("StoreName")),
                    Category = Enum.Parse<Category>(reader.GetString(reader.GetOrdinal("Category"))),
                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UserId = reader.GetGuid(reader.GetOrdinal("UserId"))
                });
            }

            return purchases;
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

            return new PurchaseRecord
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                StoreName = reader.GetString(reader.GetOrdinal("StoreName")),
                Category = Enum.Parse<Category>(reader.GetString(reader.GetOrdinal("Category"))),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UserId = reader.GetGuid(reader.GetOrdinal("UserId"))
            };
        }

        public async Task<bool> DeletePurchase(int id)
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
    }
}
