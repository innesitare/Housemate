namespace Housemate.Application.Models.Garbage;

public sealed class Waste
{
    public required DateOnly CollectionDay { get; set; }
    
    public required WasteType WasteType { get; set; }
}