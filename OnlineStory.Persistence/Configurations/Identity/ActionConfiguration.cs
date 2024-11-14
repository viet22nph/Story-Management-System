
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Persistence.Constants;


namespace OnlineStory.Persistence.Configurations.Identity;

public class ActionConfiguration: MappingEntityTypeConfiguration<Domain.Entities.Identity.Action>
{
    public override void Configure(EntityTypeBuilder<Domain.Entities.Identity.Action> builder)
    {
        builder.ToTable(TableNames.Action);
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        base.Configure(builder);
    }
}
