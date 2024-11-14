
namespace OnlineStory.Contract.Services.V1.ReadingHistory;

public class Response
{

    public record ReadingStoryResponse(
        Guid Id,
        string Title,
        string SubTitle,
        string Thumbnail,
        string Slug,
        DateTimeOffset LastReadAt
    );
}
