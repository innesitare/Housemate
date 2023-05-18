namespace Housemate.Contracts.Requests.HousingTaskRequests;

public sealed class UpdateHousingTaskRequest
{
    public required int Priority { get; init; }
}