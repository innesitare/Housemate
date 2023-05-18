using Housemate.Application.Models.Wastes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Housemate.Application.EntityConfigurations;

public sealed class WasteConfiguration : IEntityTypeConfiguration<Waste>
{
    public void Configure(EntityTypeBuilder<Waste> builder)
    {
        builder.ToTable("Wastes")
            .HasKey(w => w.CollectionDay);

        builder.Property(w => w.CollectionDay)
            .IsRequired();

        builder.Property(w => w.WasteType)
            .IsRequired();
    }
}