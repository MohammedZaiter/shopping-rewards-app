
namespace Shop.Rewards
{
    using Shop.Rewards.Models;
    using Shop.Rewards.Store;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class PurchaseManager : IPurchaseManager
    {
        private readonly IStore store;

        public PurchaseManager(IStore store)
        {
            this.store = store ?? throw new System.ArgumentNullException(nameof(store));
        }

        public Task CreatePurchase(CreatePurchaseRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.store.CreateNewPurchase(
                PurchaseRecord.ConvertToPurchaseRecord(request));
        }

        public Task UpdatePurchase(Guid id, UpdatePurchaseRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this.store.UpdatePurchase(id,
                PurchaseRecord.ConvertToPurchaseRecord(request));
        }

        public async Task<PurchaseCollection> GetAllPurchasesByPageNumber(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new InvalidOperationException($"Page number cannor be less than 1.");
            }

            var fetchRecordsResult = await this.store.GetAllPurchases(pageNumber);

            var purchases = new List<Purchase>();
            foreach (var purchase in fetchRecordsResult.Records)
            {
                purchases.Add(
                    PurchaseRecord.ConvertToPurchaseModel(purchase));
            }

            return new PurchaseCollection(purchases, fetchRecordsResult.TotalCount, fetchRecordsResult.NestPageNumber);

        }

        public async Task<Purchase> GetPurchaseById(Guid id)
        {
            var purchase = await this.store.GetPurchaseById(id);

            return PurchaseRecord.ConvertToPurchaseModel(purchase);
        }

        public Task DeletePurchase(Guid id)
        {
            return this.store.DeletePurchase(id);
        }
    }
}
