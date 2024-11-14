

using OnlineStory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Persistence.Constants;


namespace OnlineStory.Persistence.Configurations;

public class NotificationConfiguration: MappingEntityTypeConfiguration<Notification>
{
    public override void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(TableNames.Notification);
        builder.HasKey(t => t.Id);
        builder.Property(x=> x.Message).HasMaxLength(256).IsRequired();
        builder.Property(x=> x.Type).IsRequired().HasComment("Notification types: NewChapter, Comment, ChapterUpdate, System");
        builder.Property(x => x.Link).HasMaxLength(500);
        base.Configure(builder);
    }
}
