
namespace Shop.Rewards.Store
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStore
    {
        Task CreateNewPurchase(PurchaseRecord purchase);

        Task<PurchaseRecord> GetPurchaseById(Guid id);

        Task<IEnumerable<PurchaseRecord>> GetAllPurchases(int pageNumber);

        Task<bool> UpdatePurchase(Guid id, PurchaseRecord purchase);

        Task<bool> DeletePurchase(int id);
    }
}
