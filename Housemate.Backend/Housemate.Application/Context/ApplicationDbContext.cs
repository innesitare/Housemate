using System.Reflection;
using Housemate.Application.Models.HousingTasks;
using Housemate.Application.Models.StudentInfo;
using Housemate.Application.Models.Wastes;
using Microsoft.EntityFrameworkCore;

namespace Housemate.Application.Context;

public class ApplicationDbContext : DbContext
{
    public required DbSet<Student> Students { get; init; }
    public required DbSet<HousingTask> HousingTasks { get; init; }
    public required DbSet<Waste> Wastes { get; init; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}