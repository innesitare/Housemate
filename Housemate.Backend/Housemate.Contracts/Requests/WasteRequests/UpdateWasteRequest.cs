namespace Housemate.Contracts.Requests.WasteRequests;

public sealed class UpdateWasteRequest
{
    public required DateOnly CollectionDay { get; init; }
    
    public required int WasteType { get; init; }
}