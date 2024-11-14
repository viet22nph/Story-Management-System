
using Microsoft.EntityFrameworkCore;
using OnlineStory.Contract.Extensions;
using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Abstractions.Entities;
using OnlineStory.Domain.Enums;
namespace OnlineStory.Domain.Entities;

public class Story : EntityBase<Guid>, IDateTracking, ISoftDelete
{
 
    public string StoryTitle { get; set; } // tên truyện
    public string? AnotherStoryTitle { get; set; } // tên khác
    public string? Description { get; set; } // mô tả ngắn
    public string? Author { get; set; } // tác giả
    public string Thumbnail { get; set; } // ảnh bìa
    public string Slug { get; set; }// đừng dẫn url
    public int CountryId { get; set; }
    public StoryStatus StoryStatus { get; set; }
    public Audience Audience { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }

    public virtual Country? Country { get; set; }

    private  List<StoryGenre> _storyGenres;
    public IReadOnlyCollection<StoryGenre> StoryGenres => _storyGenres.AsReadOnly();
    private List<Chapter> _chapters;
    public IReadOnlyCollection<Chapter> Chapters => _chapters.AsReadOnly();
    private List<StoryView> _storyViews;
    public IReadOnlyCollection<StoryView> StoryViews => _storyViews.AsReadOnly();

    private readonly List<ReadingHistory> _histories;
    public IReadOnlyCollection<ReadingHistory> Histories => _histories.AsReadOnly();

    private readonly List<UserStoryTracking> _followStories;
    public IReadOnlyCollection<UserStoryTracking> FollowStories => _followStories.AsReadOnly();
    private readonly List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private readonly List<StoryRecommendations> _storyRecommendations;
    public IReadOnlyCollection<StoryRecommendations> StoryRecommendations => _storyRecommendations.AsReadOnly();

    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    protected Story()
    {
        _storyGenres = new List<StoryGenre>();
        _histories = new List<ReadingHistory>();
        _followStories = new List<UserStoryTracking>();
        _comments = new List<Comment>();
        _storyViews = new List<StoryView>();
        _chapters = new List<Chapter>();
        _storyRecommendations = new List<StoryRecommendations>();
    }
    public Story(string storyTitle,
        string? anotherStoryTitle,
        string? description,
        string? author,
        string thumbnail,
        int countryId,
        StoryStatus? storyStatus,
        Audience? audience,
        string? slug
        ) : this()
    {
        StoryTitle = !string.IsNullOrWhiteSpace(storyTitle.Trim())? storyTitle.Trim(): throw new ArgumentNullException(nameof(storyTitle));

        AnotherStoryTitle = anotherStoryTitle?.Trim();
        Description = description;
        Author = author?.Trim();
        Thumbnail = !string.IsNullOrWhiteSpace(thumbnail.Trim()) ? thumbnail.Trim() : throw new ArgumentNullException(nameof(thumbnail)); ;
        CountryId = countryId;
        StoryStatus = storyStatus ?? StoryStatus.Updating;
        Audience = audience ?? Audience.Both;
        Slug = !string.IsNullOrWhiteSpace(slug) ? slug : slug.ToSlug();
    }



    public static Story Create(
        string storyTitle, 
        string? anotherStoryTitle,
        string? description,
        string? author, 
        string thumbnail,
        int countryId, 
        StoryStatus? storyStatus,
        Audience? audience, string? slug)
    {
        return new Story
        {
            StoryTitle = !string.IsNullOrWhiteSpace(storyTitle.Trim()) ? storyTitle.Trim() : throw new ArgumentNullException(nameof(storyTitle)),
            AnotherStoryTitle = anotherStoryTitle?.Trim(),
            Description = description,
            Author = author?.Trim(),
            Thumbnail = !string.IsNullOrWhiteSpace(thumbnail.Trim()) ? thumbnail.Trim() : throw new ArgumentNullException(nameof(thumbnail)),
            CountryId = countryId,
            StoryStatus = storyStatus?? StoryStatus.Updating,
            Audience = audience?? Audience.Both,
            CreatedDate = DateTimeOffset.UtcNow,
            ModifiedDate = DateTimeOffset.UtcNow,
            Slug = !string.IsNullOrWhiteSpace(slug) ? slug : storyTitle.ToSlug()
        };
    }
    public void SetStoryGenresByGenreIds(List<int>? genreIds)
    {
        if (genreIds == null)
            return;  
        
        // Clear existing genres
        _storyGenres.Clear();

        // Add new genres based on the provided IDs
        foreach (var genreId in genreIds)
        {
            _storyGenres.Add(new StoryGenre
            {
                StoryId = this.Id, // Assuming the Story entity has an Id
                GenreId = genreId
            });
        }
    }
    public void Update(
    string? storyTitle = null,
    string? anotherStoryTitle = null,
    string? description = null,
    string? author = null,
    string? thumbnail = null,
    int? countryId = null,
    StoryStatus? storyStatus = null,
    Audience? audience = null )
    {
        if (!string.IsNullOrWhiteSpace(storyTitle))
            StoryTitle = storyTitle;
            AnotherStoryTitle = anotherStoryTitle;   
            Description = description;
            Author = author;

        if (!string.IsNullOrWhiteSpace(thumbnail))
            Thumbnail = thumbnail;

        if (countryId.HasValue)
            CountryId = countryId.Value;
         
        if (storyStatus.HasValue)
            StoryStatus = storyStatus.Value;

        if (audience.HasValue)
            Audience = audience.Value;
        ModifiedDate = DateTimeOffset.UtcNow;

        ModifiedDate = DateTimeOffset.UtcNow;
    }
    public async Task IncrementViewAsync()
    {
        await _semaphore.WaitAsync();

        try
        {
            var today = DateTime.Today;
            var storyView = _storyViews.FirstOrDefault(x => x.ViewDate == today);

            if (storyView is not null)
            {
                // Nếu đã có bản ghi trong ngày, tăng ViewCount
                storyView.Count++;
            }
            else
            {
                // Nếu chưa có bản ghi, tạo mới
                storyView = new StoryView(today);
                _storyViews.Add(storyView);
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

}
