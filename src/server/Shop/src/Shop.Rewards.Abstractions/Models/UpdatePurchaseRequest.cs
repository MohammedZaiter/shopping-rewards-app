
namespace Shop.Rewards.Models
{
    using System;

    public class UpdatePurchaseRequest : PurchaseRequest
    {
        public UpdatePurchaseRequest(Guid userId, string storeName, Category category, decimal amount) : base(userId, storeName, category, amount) { }
    }
}