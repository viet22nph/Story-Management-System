

using Domain.Entities;
using OnlineStory.Contract.Extensions;
using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Abstractions.Entities;
namespace OnlineStory.Domain.Entities;

public class Chapter : EntityBase<int>, IDateTracking, IUserTracking
{
    public int ChapterNumber { get; set; }
    public string ChapterTitle { get; set; }
    public string Slug { get; set; }
    public Guid StoryId { get; set; }
    public virtual Story Story { get; set; }
    private List<ChapterImage> _images;
    public IReadOnlyCollection<ChapterImage> Images=> _images.AsReadOnly();

    private readonly List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    private readonly List<ReadingHistory> _readingHistories;
    public IReadOnlyCollection<ReadingHistory> ReadingHistories => _readingHistories.AsReadOnly();

    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public Guid CreateBy { get; set; }
    public Guid? UpdateBy { get; set; }

    private Chapter()
    {
        _images = new List<ChapterImage>();   
        _comments = new List<Comment>();
        _readingHistories = new List<ReadingHistory>();
    }
    public Chapter(int chapterNumber, string chapterTitle,Story story, Guid userCreate): this()
    {
        ChapterNumber = chapterNumber;
        ChapterTitle = chapterTitle;
        Slug = chapterTitle.ToSlug();
        Story = story ?? throw new ArgumentNullException(nameof(story));
        StoryId = story.Id;
        CreatedDate = DateTimeOffset.UtcNow;
    }
    public void AddImage(ChapterImage image)
    {
        if(image is null)
        {
            throw new ArgumentNullException(nameof (image));
        }    
        if (_images.Any(img => img.ImageUrl == image.ImageUrl))
        {
            throw new InvalidOperationException("Image already exists in this chapter.");
        }
        _images.Add(image);
    }
    public void AddImageRange(List<ChapterImage> images)
    {
        foreach(var image in images)
        {
            AddImage(image);
        }
    }
    public void RemoveImage(string imageUrl)
    {
        var image = _images.FirstOrDefault(img => img.ImageUrl == imageUrl);
        if (image is not null)
        {
            _images.Remove(image);
        }
    }
    public void RemoveImage(ChapterImage image)
    {
        if(image is null)
        {
            return;
        }
        var imageRemove = _images.FirstOrDefault(img => img.Id == image.Id && img.ImageUrl == image.ImageUrl);
        if (imageRemove is not null)
        {
            _images.Remove(imageRemove);
        }
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Chapter chapter = (Chapter)obj;
        return Id == chapter.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    public void Update(int chapterNumber, string chapterTitle)
    {
        ChapterNumber = chapterNumber;
        ChapterTitle = chapterTitle;
        Slug = chapterTitle.ToSlug();
        ModifiedDate = DateTimeOffset.UtcNow;
    }
}
