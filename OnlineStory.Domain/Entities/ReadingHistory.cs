
using Domain.Entities.Identity;
using OnlineStory.Domain.Abstractions;

namespace OnlineStory.Domain.Entities;

public class ReadingHistory: EntityBase<int>
{

    public Guid StoryId { get; set; }
    public int ChapterId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset LastReadAt {  get; set; }
    
    public virtual Story? Story { get; set; }
    public virtual Chapter? Chapter { get; set; }
    public virtual AppUser? User { get; set; }

    private ReadingHistory()
    {

    }
    public ReadingHistory(Guid storyId, int chapterId, Guid userId): this()
    {
        StoryId = storyId;
        ChapterId = chapterId;
        UserId = userId;
        LastReadAt = DateTimeOffset.UtcNow;
    }
    public void Update()
    {
        LastReadAt = DateTimeOffset.UtcNow;
        
    }
}
