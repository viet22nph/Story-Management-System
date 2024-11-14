

using OnlineStory.Contract.Share;
using StackExchange.Redis;

namespace OnlineStory.Contract.Extensions;

public static class OrderSortExtentsions
{
    public static SortOrder GetSortOrder(this string orderBy)
    {
        return !string.IsNullOrWhiteSpace(orderBy) ? orderBy.ToLower().Equals("asc") ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;
    }
}
