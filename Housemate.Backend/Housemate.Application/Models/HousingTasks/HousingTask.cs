namespace Housemate.Application.Models.HousingTasks;

public sealed class HousingTask
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required DateTime AssignedAt { get; init; }
    
    public required TaskPriority Priority { get; init; }
}