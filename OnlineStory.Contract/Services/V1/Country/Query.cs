

using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Country.Response;

namespace OnlineStory.Contract.Services.V1.Country;

public class Query
{
    public record GetCountiesQuery(string? SortColumn, SortOrder SortBy) : IQuery<List<CountryResponse>>;
}

