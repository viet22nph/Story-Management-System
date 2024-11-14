using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Enums;

namespace OnlineStory.Domain.Entities;

public class Notification : EntityBase<int>
{
    public NotificationType Type { get; set; }
    public string Message {  get; set; }
    public string? Link { get; set; }           
    public DateTimeOffset CreateAt { get; set; }
    public bool IsGlobal { get; set; }
    private readonly List<UserNotification> _userNotifications;

    public IReadOnlyCollection<UserNotification> UserNotifications => _userNotifications.AsReadOnly();

    private Notification()
    {
        _userNotifications = new List<UserNotification>();
        IsGlobal = false;
    }
    public Notification(NotificationType type, string message, string? link): this()
    {
        
        Type = type;
        Message =  !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentNullException(nameof(message)); 
        Link = link;
        CreateAt = DateTimeOffset.UtcNow;
    }

    public void AddNewUserNotification(UserNotification userNotification)
    {
        if (userNotification is null)
            return;
        // đã được gửi thông báo
        if(_userNotifications.FirstOrDefault(x=> x.UserReceiveId == userNotification.UserReceiveId) is not null)
        {
            return;
        }    
        _userNotifications.Add(userNotification);
    }


}
