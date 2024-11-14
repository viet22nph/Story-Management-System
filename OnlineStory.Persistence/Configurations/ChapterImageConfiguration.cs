using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Persistence.Configurations;
using OnlineStory.Persistence.Constants;


namespace Persistence.Configurations
{
    public class ChapterImageConfiguration : MappingEntityTypeConfiguration<ChapterImage>
    {
        public override void Configure(EntityTypeBuilder<ChapterImage> builder)
        {
            builder.ToTable(TableNames.ChapterImage);
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.ImageUrl).IsRequired().HasMaxLength(255);
            builder.HasOne(x=> x.Chapter)
                .WithMany(x=> x.Images)
                .HasForeignKey(x=> x.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
