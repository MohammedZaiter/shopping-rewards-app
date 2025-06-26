
namespace Shop.Rewards.Search
{
    using Shop.Rewards.Common;
    using System.Threading.Tasks;

    public abstract class SearchQueryBuilder : ISearchQueryBuilder
    {
        protected int PageNumberValue
        {
            get;
            private set;
        }

        protected int PageSizeValue
        {
            get;
            private set;
        }

        public ISearchQueryBuilder PageNumber(int pageNumber)
        {
            PageNumberValue = pageNumber < 1 ? 1 : pageNumber;
            return this;
        }

        public ISearchQueryBuilder PageSize(int pageSize)
        {
            PageSizeValue = pageSize <= 0 ? 10 : pageSize;
            return this;
        }

        public abstract Task<ICollection> Execute();
    }
}
