using Housemate.Application.Models.HousingTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Housemate.Application.EntityConfigurations;

public sealed class HousingTaskConfiguration : IEntityTypeConfiguration<HousingTask>
{
    public void Configure(EntityTypeBuilder<HousingTask> builder)
    {
        builder.ToTable("HousingTasks")
            .HasKey(ht => ht.Id);

        builder.Property(ht => ht.Id)
            .IsRequired();

        builder.Property(ht => ht.Name)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(ht => ht.Description)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Property(ht => ht.CreatedAt)
            .IsRequired();

        builder.Property(ht => ht.Priority)
            .IsRequired();
    }
}