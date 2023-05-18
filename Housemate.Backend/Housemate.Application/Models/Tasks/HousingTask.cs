namespace Housemate.Application.Models.Tasks;

public sealed class HousingTask
{
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required DateTime AssignedAt { get; init; }
    
    public required TaskPriority Priority { get; init; }
}