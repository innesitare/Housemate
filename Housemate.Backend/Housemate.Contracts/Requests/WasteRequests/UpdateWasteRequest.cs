namespace Housemate.Contracts.Requests.WasteRequests;

public sealed class UpdateWasteRequest
{
    public required DateOnly CollectionDay { get; set; }
    
    public required int WasteType { get; set; }
}