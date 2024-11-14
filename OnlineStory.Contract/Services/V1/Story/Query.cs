

using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Story.Response;

namespace OnlineStory.Contract.Services.V1.Story;

public class Query
{
    public record GetStoriesQuery(string? SearchTerm, string? SortColumn, SortOrder SortOrder, int PageIndex, int PageSize) : IQuery<Pagination<StoryResponse>>;
    public record GetStoryByIdQuery(Guid Id):IQuery<StoryDetailResponse>;
    public record GetStoryBySlugQuery(string Slug) : IQuery<StoryDetailResponse>;
    public record GetStoriesByGenreQuery(string GenreSlug, string? Status, string? SortColumn, SortOrder SortOrder, int PageIndex, int  PageSize) : IQuery<Pagination<StoryResponse>>;
}
