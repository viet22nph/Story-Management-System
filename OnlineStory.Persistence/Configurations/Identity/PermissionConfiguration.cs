
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations.Identity;

public class PermissionConfiguration: MappingEntityTypeConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permission);
        builder.HasKey(rp => new { rp.RoleId, rp.ActionId, rp.ResourceId });
        builder.HasOne(x => x.Role)
            .WithMany(x => x.Permissions)
            .HasForeignKey(x => x.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Action)
          .WithMany(x => x.Permissions)
          .HasForeignKey(x => x.ActionId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Resource)
         .WithMany(x => x.Permissions)
         .HasForeignKey(x => x.ResourceId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
        base.Configure(builder);
    }
}
