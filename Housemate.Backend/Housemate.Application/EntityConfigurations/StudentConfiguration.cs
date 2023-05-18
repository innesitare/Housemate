using Housemate.Application.Models.StudentInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Housemate.Application.EntityConfigurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students")
            .HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);
        
        builder.Property(s => s.LastName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(s => s.Email)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(s => s.Password)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(s => s.Birthdate)
            .IsRequired();

        builder.HasIndex(s => s.Id)
            .IsDescending()
            .IsUnique();

        builder.HasMany(s => s.HousingTasks)
            .WithOne()
            .HasForeignKey("StudentId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}