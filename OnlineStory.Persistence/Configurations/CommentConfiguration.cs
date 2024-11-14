
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations;

public class CommentConfiguration : MappingEntityTypeConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(TableNames.Comment);
        builder.HasKey(c => c.Id);
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x=> x.ParentCommentId).IsRequired(false).HasComment("Id comment parent if null this is parent");
        builder.Property(x=> x.Type).IsRequired().HasComment("Loại bình luận: 1 là bình luận truyện, 2 là bình luận chương");
        builder.Property(x => x.ChapterId).IsRequired(false);
        builder.Property(x=> x.StoryId).IsRequired(false);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x=> x.Chapter)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x=> x.ParentComment)
            .WithMany(x=>x.Replies)
            .HasForeignKey(x => x.ParentCommentId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Story)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.StoryId)
            .OnDelete(DeleteBehavior.NoAction);
        base.Configure(builder);
    }

}
