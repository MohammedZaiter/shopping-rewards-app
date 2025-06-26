
namespace Shop.Rewards.Search
{
    using Microsoft.Data.SqlClient;
    using Shop.Rewards.Models;
    using Shop.Rewards.Search.Abstractions;
    using Shop.Rewards.Store;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class PurchaseSearchQueryBuilder : SearchQueryBuilder
    {
        private Guid? userId;
        private Category? category;

        private readonly ISearchStore store;

        public PurchaseSearchQueryBuilder(ISearchStore store)
        {
            this.store = store;
        }

        public PurchaseSearchQueryBuilder UserId(Guid userId)
        {
            this.userId = userId;

            return this;
        }

        public PurchaseSearchQueryBuilder Category(Category category)
        {
            this.category = category;

            return this;
        }

        public override async Task<PurchaseCollection> Execute()
        {
            var parameters = new Dictionary<string, object>
            {
                ["@PageNumber"] = this.PageNumberValue,
                ["@PageSize"] = this.PageSizeValue,
                ["@UserId"] = this.userId.HasValue ? this.userId.Value : DBNull.Value,
                ["@Category"] = this.category?.ToString()
            };

            return await store.ExecuteSearchQuery("usp_GetAllPurchases", parameters);
        }

        private async Task<PurchaseCollection> ToPurchaseCollection(SqlDataReader reader)
        {
            var purchases = new List<PurchaseRecord>();
            int totalCount = 0;

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

            return new PurchaseCollection(purchases, totalCount, this.PageNumberValue + 1);
        }

        private PurchaseRecord ReadPurchaseRecordFromReader(SqlDataReader reader)
        {
            return new PurchaseRecord(reader.GetGuid(reader.GetOrdinal("Id")), reader.GetGuid(reader.GetOrdinal("UserId")), reader.GetString(reader.GetOrdinal("StoreName")),
                         Enum.Parse<Category>(reader.GetString(reader.GetOrdinal("Category"))), reader.GetDecimal(reader.GetOrdinal("Amount")), reader.GetDateTime(reader.GetOrdinal("CreatedAt")));
        }
    }
}