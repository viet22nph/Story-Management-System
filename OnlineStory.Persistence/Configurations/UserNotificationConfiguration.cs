
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;
namespace OnlineStory.Persistence.Configurations;

//public class UserNotificationConfiguration: MappingEntityTypeConfiguration<UserNotification>
//{
//    public override void Configure(EntityTypeBuilder<UserNotification> builder)
//    {
//        builder.ToTable(TableNames.UserNotification);
//        builder.HasKey(x => new { x.UserReceiveId, x.NotificationId });
//        builder.Property(x=>x.IsRead).IsRequired().HasDefaultValue(false);
//        builder.HasOne(x=>x.UserReceive)
//            .WithMany(x=> x.UserNotifications)
//            .HasForeignKey(x=>x.UserReceiveId)
//            .OnDelete(DeleteBehavior.Cascade);
//        builder.HasOne(x=> x.Notification)
//            .WithMany(x=>x.UserNotifications)
//            .HasForeignKey(x=> x.NotificationId)
//            .OnDelete(DeleteBehavior.Cascade);
//        builder.HasIndex(x => new { x.UserReceiveId, x.NotificationId }).IsUnique();
//        base.Configure(builder);
//    }
//}
