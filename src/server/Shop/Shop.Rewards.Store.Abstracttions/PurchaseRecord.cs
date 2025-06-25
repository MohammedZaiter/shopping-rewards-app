
namespace Shop.Rewards.Store
{
    using System;

    public class PurchaseRecord
    {
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
