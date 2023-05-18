namespace Housemate.Application.Models.Wastes;

public sealed class Waste
{
    public required DateOnly CollectionDay { get; set; }
    
    public required WasteType WasteType { get; set; }
}