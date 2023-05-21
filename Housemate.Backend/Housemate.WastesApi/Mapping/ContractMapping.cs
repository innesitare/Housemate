using Housemate.Application.Models.Wastes;
using Housemate.Contracts.Requests.WasteRequests;
using Housemate.Contracts.Responses.WasteResponses;

namespace Housemate.WastesApi.Mapping;

public static class ContractMapping
{
    public static Waste MapToWaste(this CreateWasteRequest wasteRequest)
    {
        return new Waste
        {
            CollectionDay = wasteRequest.CollectionDay,
            WasteType = (WasteType) wasteRequest.WasteType
        };
    }
    
    public static Waste MapToWaste(this UpdateWasteRequest wasteRequest, string wasteId)
    {
        return new Waste
        {
            Id = wasteId,
            CollectionDay = wasteRequest.CollectionDay,
            WasteType = (WasteType) wasteRequest.WasteType
        };
    }

    public static WasteResponse MapToResponse(this Waste waste)
    {
        return new WasteResponse
        {
            Id = waste.Id,
            CollectionDay = waste.CollectionDay,
            WasteType = (int) waste.WasteType
        };
    }
}