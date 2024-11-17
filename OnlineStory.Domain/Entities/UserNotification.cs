

using Domain.Entities.Identity;

namespace OnlineStory.Domain.Entities;

//public class UserNotification
//{
//    public Guid UserReceiveId { get; set; }
//    public int NotificationId {  get; set; }
//    public bool IsRead { get; set; } = false;
//    public DateTimeOffset ReceivedDate {  get; set; }
//    public virtual AppUser? UserReceive { get; set; }
//    public virtual Notification? Notification { get; set; }

//    public UserNotification( Guid userReceiveId,  int notificationId)
//    {
//        UserReceiveId = userReceiveId;
//        NotificationId = notificationId;
//        IsRead = false;
//        ReceivedDate = DateTimeOffset.UtcNow;
//    }
//    public void MarkAsRead()
//    {
//        IsRead = true;
//    }
//}
