namespace Housemate.Contracts.Responses.WasteResponses;

public sealed class WasteResponse
{
    public required string Id { get; init; }
    
    public required DateOnly CollectionDay { get; init; }
    
    public required int WasteType { get; init; }
}