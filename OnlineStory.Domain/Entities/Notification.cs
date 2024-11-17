using Domain.Entities.Identity;
using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Abstractions.Entities;
using OnlineStory.Domain.Enums;

namespace OnlineStory.Domain.Entities;

public class Notification : EntityBase<int>, IDateTracking
{
    public Guid UserReceiveId {  get; set; }
    public NotificationType Type { get; set; }
    public string Message {  get; set; }
    public string? Link { get; set; }           
    public DateTimeOffset CreateAt { get; set; }
    public bool IsGlobal { get; set; }
    public bool IsBatch { get; set; } 
    public string? RelatedData {  get; set; }
    public bool IsRead { get; set; } = false;
    public virtual AppUser? UserReceive { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }


    private Notification()
    {
        IsGlobal = false;
        IsBatch = false;
        IsRead = false;
    }
    public Notification(Guid userReceiveId,NotificationType type, string message, string? link,string? relateData): this()
    {
        UserReceiveId = userReceiveId;
        Type = type;
        Message =  !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentNullException(nameof(message)); 
        Link = link;
        CreateAt = DateTimeOffset.UtcNow;
        RelatedData = relateData;
    }
    public void SetIsBatch(bool value = true)
    {
        IsBatch = value;
    }

}
