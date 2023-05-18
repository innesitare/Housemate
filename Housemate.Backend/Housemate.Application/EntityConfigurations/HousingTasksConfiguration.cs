using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Housemate.Application.EntityConfigurations;

public class HousingTasksConfiguration : IEntityTypeConfiguration<HousingTasksConfiguration>
{
    public void Configure(EntityTypeBuilder<HousingTasksConfiguration> builder)
    {
        throw new NotImplementedException();
    }
}