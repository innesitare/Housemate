namespace Housemate.Contracts.Responses.HousingTaskResponses;

public sealed class HousingTaskResponse
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required int Priority { get; init; }
    
    public required DateTime CreatedAt { get; init; }
}