using Microsoft.EntityFrameworkCore;

namespace OnlineStory.Persistence.Configurations;

public interface IMappingConfiguration
{
    void ApplyConfiguration(ModelBuilder modelBuilder);
}
