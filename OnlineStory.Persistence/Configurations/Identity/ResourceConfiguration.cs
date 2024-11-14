
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations.Identity;

public class ResourceConfiguration: MappingEntityTypeConfiguration<Resource>
{
    public override void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(TableNames.Resource);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasConversion(typeof(string)).IsRequired();
        base.Configure(builder);
    }
}
