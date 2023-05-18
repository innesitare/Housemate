namespace Housemate.Contracts.Requests.HousingTaskRequests;

public sealed class CreateHousingTaskRequest
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required int Priority { get; init; }
    
    public required DateTime AssignedAt { get; init; }
}