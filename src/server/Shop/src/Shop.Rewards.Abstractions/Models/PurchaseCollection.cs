
namespace Shop.Rewards.Models
{
    using Shop.Rewards.Common;
    using System.Collections.Generic;

    public class PurchaseCollection : Collection<Purchase>
    {
        public PurchaseCollection(List<Purchase> purchases,  int totalCount, int nextPageNumber): base(purchases)
        {
            this.TotalCount = totalCount;
            this.NextPageNumber = nextPageNumber;
        }

        public int TotalCount
        {
            get;
            set;
        }

        public int NextPageNumber
        {
            get;
            set;
        }
    }
}
