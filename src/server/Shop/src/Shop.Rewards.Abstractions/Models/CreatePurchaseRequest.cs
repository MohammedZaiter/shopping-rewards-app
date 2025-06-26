
namespace Shop.Rewards.Models
{
    using System;

    public class CreatePurchaseRequest : PurchaseRequest
    {
        public CreatePurchaseRequest(Guid userId, string storeName, Category category, decimal amount) : base(userId, storeName, category, amount) { }
    }
}
