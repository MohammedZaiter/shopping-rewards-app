
namespace Shop.Rewards.Store
{
    using System;
    using System.Threading.Tasks;

    public interface IStore
    {
        Task CreateNewPurchase(PurchaseRecord purchase);

        Task<PurchaseRecord> GetPurchaseById(Guid id);

        Task<PurchaseRecordCollection> GetAllPurchases(int pageNumber);

        Task<bool> UpdatePurchase(Guid id, PurchaseRecord purchase);

        Task<bool> DeletePurchase(Guid id);
    }
}
