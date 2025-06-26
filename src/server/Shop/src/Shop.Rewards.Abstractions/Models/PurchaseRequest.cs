
namespace Shop.Rewards.Models
{
    using System;

    public class PurchaseRequest
    {
        public PurchaseRequest(Guid userId, string storeName, Category category, decimal amount)
        {
            this.UserId = userId;
            this.StoreName = storeName ?? throw new ArgumentNullException(nameof(userId));
            this.Category = category;
            this.Amount = amount;
        }

        public Guid UserId
        {
            get;
            set;
        }

        public string StoreName
        {
            get;
            set;
        }

        public Category Category
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }
    }
}
