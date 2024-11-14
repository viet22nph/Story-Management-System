
namespace OnlineStory.Persistence.Constants;

public static class TableNames
{
    // identity table name
    internal const string AppUser = nameof(AppUser);
    internal const string AppRole = nameof(AppRole);
    internal const string AppUserRole = nameof(AppUserRole);
    internal const string AppRoleClaim = nameof(AppRoleClaim);
    internal const string AppUserClaim = nameof(AppUserClaim);
    internal const string AppUserToken = nameof(AppUserToken);
    internal const string AppUserLogin = nameof(AppUserLogin);
    
    // ****** Singular nouns ********
    
    internal const string Story = nameof(Story);
    internal const string Chapter = nameof(Chapter);
    internal const string ChapterImage = nameof(ChapterImage);
    internal const string Country = nameof(Country);
    internal const string StoryGenre= nameof(StoryGenre);
    internal const string Genre = nameof(Genre);
    internal const string StoryView = nameof(StoryView);    
    internal const string ReadingHistory = nameof(ReadingHistory);
    internal const string UserStoryTracking = nameof(UserStoryTracking);
    internal const string Comment = nameof(Comment);
    internal const string UserNotification = nameof(UserNotification);
    internal const string Notification = nameof(Notification);
    internal const string StoryRecommendations = nameof(StoryRecommendations);
    //

    internal const string Action = nameof(Action);
    internal const string Resource = nameof(Resource);
    internal const string Permission = nameof(Permission);
}
