
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineStory.Application.Abstractions;
using OnlineStory.Application.Abstractions.Repository;
using OnlineStory.Contract.Abstractions.Message;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Enums;
using static OnlineStory.Contract.Services.V1.Notification.Event;

namespace OnlineStory.Application.UserCases.V1.Events.Notification;

public class NotificationChapterAddedEventHandler : IEventHandler<NotificationChapterAddedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeSpan BatchDuration = TimeSpan.FromMinutes(30);
    public NotificationChapterAddedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(NotificationChapterAddedEvent notification, CancellationToken cancellationToken)
    {
        // Lấy danh sách người theo dõi truyện từ repository
        var followers = await _unitOfWork.UserFollowStoryRepository.FindAll(x=> x.StoryId == notification.StoryId && x.IsNotificationEnabled == true).AsNoTracking().ToListAsync(cancellationToken);

        // Lấy thông báo nhóm gần nhất
        

        var story = await _unitOfWork.StoryRepository.FindByIdAsync(notification.StoryId);
        if(story is null)
        {
            return;
        }
        var userIds = followers.Select(f => f.UserId).ToList();
        var existingBatches = await _unitOfWork.NotificationRepository.GetBatchNotificationsForUsersAsync(userIds, notification.StoryId);
        // Tạo và gửi thông báo cho từng người theo dõi
        foreach (var follower in followers)
        {
            var existingBatch = existingBatches.FirstOrDefault(b => b.UserReceiveId == follower.UserId);
            await AddBatchNotificationWithTimeAsync(existingBatch,userId: follower.UserId, notification.StoryId, notification.ChapterTitle, story.StoryTitle);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }

    private async Task AddBatchNotificationWithTimeAsync(Domain.Entities.Notification existingBatch,Guid userId, Guid storyId, string chapterTitle, string storyTitle)
    {
      
        if (existingBatch is not null && DateTime.UtcNow - existingBatch.CreatedDate < BatchDuration)
        {
            // Cập nhật thông báo hiện tại nếu chưa quá 5 phút

            var relatedData = JsonConvert.DeserializeObject<List<dynamic>>(existingBatch.RelatedData) ?? new List<dynamic>();
            relatedData.Add(new { UserId = userId, StoryId = storyId, ChapterTitle = chapterTitle });
            existingBatch.RelatedData = JsonConvert.SerializeObject(relatedData);

            // Cập nhật nội dung và thời gian cuối
            var chaperTitle = relatedData.Select(r => r.ChapterTitle).Distinct().ToList();
            existingBatch.Message = $"{storyTitle} đã cập nhật {chaperTitle.Count} chương mới: {string.Join(", ", chaperTitle)}.";
            existingBatch.ModifiedDate = DateTime.UtcNow;
            existingBatch.IsBatch = true;
            _unitOfWork.NotificationRepository.Update(existingBatch);
        }
        else
        {
            var relatedData = JsonConvert.SerializeObject(new List<dynamic> { new { UserId = userId, StoryId = storyId, ChapterTitle = chapterTitle } });
            var notification = new Domain.Entities.Notification(userId, NotificationType.NewChapter, $"Truyện {storyTitle} vừa cập nhật chương mới.",null, relatedData);
            notification.SetIsBatch();
            _unitOfWork.NotificationRepository.Add(notification);
        }
    }
}
