
using Microsoft.AspNetCore.Identity;
using OnlineStory.Domain.Abstractions.Entities;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Domain.Enums;

namespace Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISoftDelete
    {


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string DisplayName { get; set; }
        public string? Avatar { get; set; }
        public Gender? Gender { get; set; }


        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> UserTokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        private readonly List<ReadingHistory> _readingHistories;
        public IReadOnlyCollection<ReadingHistory> ReadingHistories => _readingHistories.AsReadOnly();

        private readonly List<UserStoryTracking> _userStoryTrackings;
        public IReadOnlyCollection<UserStoryTracking> UserStoryTrackings => _userStoryTrackings.AsReadOnly();

        private readonly List<Comment> _comments;
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        private readonly List<UserNotification> _userNotifications;
        public IReadOnlyCollection<UserNotification> UserNotifications => _userNotifications.AsReadOnly();

        private readonly List<StoryRecommendations> _storyRecommendations;
        public IReadOnlyCollection<StoryRecommendations> StoryRecommendations => _storyRecommendations.AsReadOnly();
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }

        private AppUser()
        {
            Claims = new List<IdentityUserClaim<Guid>>();
            Logins = new List<IdentityUserLogin<Guid>>();
            UserTokens = new List<IdentityUserToken<Guid>>();
            UserRoles = new List<AppUserRole>();
            _comments = new List<Comment>();
            _userNotifications = new List<UserNotification>();
            _userStoryTrackings = new List<UserStoryTracking>();
            _storyRecommendations = new List<StoryRecommendations>();
        }
        public static AppUser CreateNewUser(string email, string userName)
        {
            return new AppUser
            {
                Email = email,
                UserName = userName,
                DisplayName = userName,
            };
        }

    }
}
