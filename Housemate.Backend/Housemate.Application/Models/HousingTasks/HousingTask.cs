namespace Housemate.Application.Models.HousingTasks;

public sealed class HousingTask
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }

    public required TaskPriority Priority { get; init; }
}