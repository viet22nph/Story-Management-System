
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations;

public class UserStoryTrackingConfiguration:  MappingEntityTypeConfiguration<UserStoryTracking>
{
    public override void Configure(EntityTypeBuilder<UserStoryTracking> builder)
    {
        builder.ToTable(TableNames.UserStoryTracking);
        builder.HasKey(x=> new {x.StoryId, x.UserId});
        builder.Property(x => x.FollowAt);
        builder.HasOne(x=> x.User)
            .WithMany(x=> x.UserStoryTrackings)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Story)
            .WithMany(x => x.FollowStories)
            .HasForeignKey(x => x.StoryId)
            .OnDelete(DeleteBehavior.Cascade);
        base.Configure(builder);
    }
}
