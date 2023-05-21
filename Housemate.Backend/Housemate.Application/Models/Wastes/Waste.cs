namespace Housemate.Application.Models.Wastes;

public sealed class Waste
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public required DateOnly CollectionDay { get; init; }
    
    public required WasteType WasteType { get; init; }
}