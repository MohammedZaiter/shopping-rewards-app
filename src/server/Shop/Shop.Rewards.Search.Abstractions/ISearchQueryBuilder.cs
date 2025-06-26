
namespace Shop.Rewards.Search
{
    public interface ISearchQueryBuilder
    {
        ISearchQueryBuilder PageNumber(int pageNumber);

        ISearchQueryBuilder PageSize(int pageSize);
    }
}
