namespace Housemate.Contracts.Requests.WasteRequests;

public sealed class CreateWasteRequest
{
    public required DateOnly CollectionDay { get; set; }
    
    public required int WasteType { get; set; }
}