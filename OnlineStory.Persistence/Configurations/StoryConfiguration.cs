
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Domain.Enums;
using OnlineStory.Persistence.Configurations;
using OnlineStory.Persistence.Constants;
namespace Persistence.Configurations
{
    public class StoryConfiguration : MappingEntityTypeConfiguration<Story>
    {
        public override void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable(TableNames.Story);
            builder.HasKey(s => s.Id);
            builder.Property(x=> x.StoryTitle).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.AnotherStoryTitle).HasMaxLength(100);
            builder.Property(x=> x.Author).HasMaxLength(100);
            builder.Property(x => x.Thumbnail).IsRequired().HasMaxLength(255);
            builder.Property(x=> x.Slug).IsRequired().HasMaxLength(255);
            builder.Property(x => x.StoryStatus).HasConversion<string>().HasMaxLength(30);
            builder.Property(x=> x.Audience).HasConversion<string>().HasDefaultValue(Audience.Both).HasMaxLength(10);
            builder.HasOne(x=> x.Country).WithMany(y=> y.Stories).HasForeignKey(x=>x.CountryId);
            builder.HasIndex(x => x.Slug).IsUnique();

        }
    }
}
