
namespace Shop.Rewards.Models
{
    using System;

    public sealed class Purchase
    {
        public Purchase(Guid id, Guid userId, string storeName, Category category, decimal amount, DateTime createdAt)
        {
            this.Id = id;
            this.UserId = userId;
            this.StoreName = storeName ?? throw new ArgumentNullException(nameof(id));
            this.Category = category;
            this.Amount = amount;
            this.CreatedAt = createdAt;
        }

        public Guid Id
        {
            get;
            set;
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

        public DateTime CreatedAt
        {
            get;
            set;
        }
    }
}
