

using OnlineStory.Contract.Dtos.StoryDtos;

namespace OnlineStory.Contract.Services.V1.Story;

public class Response
{
    public record StoryResponse(
        Guid Id,
        string Title,
        string SubTitle,
        string? Description,
        string? Author,
        string Thumbnail,       
        string Slug,
        string Status,
        List<string> Genres,
        string Country,
        bool Nsfw,
        DateTimeOffset CreateAt,
        DateTimeOffset? UpdateAt
        );
    public record StoryDetailResponse(StoryDetailDto Story);
}
