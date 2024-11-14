

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations.Identity;

public class StoryRecommendationsConfiguration:MappingEntityTypeConfiguration<StoryRecommendations>
{
    public override void Configure(EntityTypeBuilder<StoryRecommendations> builder)
    {
        builder.ToTable(TableNames.StoryRecommendations);
        builder.HasKey(t => t.Id);
        builder.Property(x => x.Score).IsRequired();
        builder.HasOne(x => x.Story).WithMany(x => x.StoryRecommendations).HasForeignKey(x => x.StoryId);
        builder.HasOne(x => x.User).WithMany(x => x.StoryRecommendations).HasForeignKey(x => x.UserId);
        base.Configure(builder);
    }
}
