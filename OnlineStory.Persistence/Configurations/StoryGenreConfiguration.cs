
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;


namespace OnlineStory.Persistence.Configurations
{
    public class StoryGenreConfiguration : MappingEntityTypeConfiguration<StoryGenre>
    {
        public override void Configure(EntityTypeBuilder<StoryGenre> builder)
        {
          
            builder.ToTable(TableNames.StoryGenre);
            builder.HasKey(x => new { x.StoryId, x.GenreId });
            builder.HasOne(x=> x.Story).WithMany(x=> x.StoryGenres).HasForeignKey(x => x.StoryId);
            builder.HasOne(x=> x.Genre).WithMany(x => x.StoryGenres).HasForeignKey(x=> x.GenreId);
        }
    }
}
