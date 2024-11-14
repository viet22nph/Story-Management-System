
using Microsoft.EntityFrameworkCore;
using OnlineStory.Application.Abstractions;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Enums;
using static OnlineStory.Contract.Services.V1.Notification.Event;

namespace OnlineStory.Application.UserCases.V1.Events.Notification;

public class NotificationChapterAddedEventHandler : IEventHandler<NotificationChapterAddedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    public NotificationChapterAddedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(NotificationChapterAddedEvent notification, CancellationToken cancellationToken)
    {
        // Lấy danh sách người theo dõi truyện từ repository
        var followers = await _unitOfWork.UserFollowStoryRepository.FindAll(x=> x.StoryId == notification.StoryId && x.IsNotificationEnabled == true).AsNoTracking().ToListAsync(cancellationToken);
        var story = await _unitOfWork.StoryRepository.FindByIdAsync(notification.StoryId);
        // Tạo và gửi thông báo cho từng người theo dõi\
        var newNotification = new Domain.Entities.Notification(NotificationType.NewChapter, $"{story.StoryTitle} vừa cập nhật chương mới: ${notification.ChapterTitle}", null);
        foreach (var follower in followers)
        {
            newNotification.AddNewUserNotification(new UserNotification(follower.UserId, newNotification.Id));
        }
        _unitOfWork.NotificationRepository.Add(newNotification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
