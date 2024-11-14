
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations;

public class StoryViewConfiguration:MappingEntityTypeConfiguration<StoryView>
{

    public override void Configure(EntityTypeBuilder<StoryView> builder)
    {
        builder.ToTable(TableNames.StoryView);
        builder.Property(x=> x.Count).IsRequired().HasDefaultValue(1);
        builder.Property(x => x.StoryId).IsRequired();
        builder.Property(x=> x.ViewDate).IsRequired();
        // Thiết lập UNIQUE cho StoryId và ViewDate
        builder.HasIndex(sv => new { sv.StoryId, sv.ViewDate }).IsUnique();
        builder.HasOne(x => x.Story)
             .WithMany(y => y.StoryViews)
             .HasForeignKey(x => x.StoryId)
             .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
