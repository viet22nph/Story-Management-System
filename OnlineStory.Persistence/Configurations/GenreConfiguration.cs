
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;
namespace OnlineStory.Persistence.Configurations
{
    public class GenreConfiguration: MappingEntityTypeConfiguration<Genre>
    {

        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable(TableNames.Genre);
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x=> x.Slug).HasMaxLength(60);
            builder.HasIndex(x => new { x.Slug, x.Name }).IsUnique();
        }
    }
}
