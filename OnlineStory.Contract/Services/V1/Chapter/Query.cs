

using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Contract.Share;
using static OnlineStory.Contract.Services.V1.Chapter.Response;

namespace OnlineStory.Contract.Services.V1.Chapter;

public class Query
{
    public record GetChapterListPaginationQuery(string StorySlug, int PageIndex =1, int PageSize=5):IQuery<Pagination<ChapterResponse>>;
    public record GetChapterListQuery(string StorySlug): IQuery<List<ChapterResponse>>;
    public record GetChapterBySlugQuery(string StorySlug, string ChapterSlug): IQuery<ChapterDetailResponse>;

    public record GetStoryChapterListOnlyQuery(string StorySlug) : IQuery<StoryWithChapterResponse>;

}
