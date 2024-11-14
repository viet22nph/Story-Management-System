using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;
namespace OnlineStory.Persistence.Configurations
{
    public class ChapterConfiguration:MappingEntityTypeConfiguration<Chapter>
    {
        public override void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable(TableNames.Chapter);
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(255);
            builder.Property(x=> x.ChapterTitle).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ChapterNumber).IsRequired();
            builder.HasOne(x=> x.Story)
                .WithMany(y=> y.Chapters)
                .HasForeignKey(x=> x.StoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
