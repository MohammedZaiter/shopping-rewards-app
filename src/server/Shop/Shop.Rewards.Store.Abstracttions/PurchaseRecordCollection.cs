
namespace Shop.Rewards.Store
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class PurchaseRecordCollection
    {
        public PurchaseRecordCollection(IEnumerable<PurchaseRecord> records, int totalCount, int nextPageNumber)
        {
            this.Records = records;
            this.TotalCount = totalCount;
            this.TotalPageCount = this.Records.Count();
            this.NestPageNumber = nextPageNumber;
        }

        public IEnumerable<PurchaseRecord> Records
        {
            get;
            set;
        }

        public int TotalCount
        {
            get;
            set;
        }

        public int TotalPageCount
        {
            get;
            set;
        }

        public int NestPageNumber
        {
            get;
            set;
        }
    }
}
