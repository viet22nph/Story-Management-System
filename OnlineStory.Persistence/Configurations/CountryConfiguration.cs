
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStory.Domain.Entities;
using OnlineStory.Persistence.Constants;

namespace OnlineStory.Persistence.Configurations
{
    public class CountryConfiguration: MappingEntityTypeConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(TableNames.Country);
            builder.HasKey(x=> x.Id);
            builder.Property(x=> x.CountryName).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.CountryCode).IsRequired().HasMaxLength(10);
        }
    }
}
