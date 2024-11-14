
using Domain.Entities.Identity;

namespace OnlineStory.Domain.Entities;

public class UserStoryTracking
{
    public Guid UserId { get; set; }
    public Guid StoryId { get; set; }
    public DateTimeOffset FollowAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsNotificationEnabled { get; set; }
    public virtual AppUser User { get; set; }
    public virtual Story Story { get; set; }


    public UserStoryTracking(Guid userId, Guid storyId)
    {
        UserId = userId;
        StoryId = storyId;
        FollowAt = DateTimeOffset.UtcNow;
        IsNotificationEnabled = true;
    }
    public void DisableNotification()
    {
        IsNotificationEnabled = false;
    }
    public void EnableNotification()
    {
        IsNotificationEnabled = true;
    }
}


