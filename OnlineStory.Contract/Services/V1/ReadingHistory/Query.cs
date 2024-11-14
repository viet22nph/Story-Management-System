
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Response;

namespace OnlineStory.Contract.Services.V1.ReadingHistory;

public class Query
{
    public record GetUserRedingHistoryQuery(Guid UserId, int PageIndex, int PageSize): IQuery<Pagination<ReadingStoryResponse>>;
}
