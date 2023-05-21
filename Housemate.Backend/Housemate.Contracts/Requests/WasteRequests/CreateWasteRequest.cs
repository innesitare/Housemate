namespace Housemate.Contracts.Requests.WasteRequests;

public sealed class CreateWasteRequest
{
    public required DateOnly CollectionDay { get; init; }
    
    public required int WasteType { get; init; }
}