
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;


namespace OnlineStory.Persistence.Configurations;

public class ReadingHistoryConfiguration: MappingEntityTypeConfiguration<ReadingHistory>
{
    public override void Configure(EntityTypeBuilder<ReadingHistory> builder)
    {
        builder.ToTable(TableNames.ReadingHistory);
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.UserId).IsRequired();
        builder.Property(x=> x.StoryId).IsRequired();
        builder.Property(x=>x.ChapterId).IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.ReadingHistories)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Chapter)
            .WithMany(y => y.ReadingHistories)
            .HasForeignKey(x => x.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Story)
            .WithMany(y => y.Histories)
            .HasForeignKey(x => x.StoryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasIndex(x=> new
        {
            x.StoryId,
            x.UserId,
            x.ChapterId,
        }).IsUnique();
        base.Configure(builder);
    }
}
