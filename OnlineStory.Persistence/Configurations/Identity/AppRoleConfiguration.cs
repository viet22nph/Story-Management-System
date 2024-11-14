﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Persistence.Constants;
namespace OnlineStory.Persistence.Configurations;

public sealed class AppRoleConfiguration: MappingEntityTypeConfiguration<AppRole>
{
    public override void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(TableNames.AppRole);
        builder.HasKey(t => t.Id);
        builder.Property(r=> r.Name).HasMaxLength(50);
        builder.Property(r=> r.Description).HasMaxLength(255);
        builder.Property(r=> r.NormalizedName).HasMaxLength(50);
        builder.HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();
        builder.HasMany(r => r.UserRoles)
          .WithOne(ur => ur.Role) // Specify the navigation property in AppUserRole
          .HasForeignKey(ur => ur.RoleId)
          .IsRequired(); // RoleId in UserRoles is required
    }
}
