using Housemate.Application.Models.HousingTasks;
using Housemate.Contracts.Requests.HousingTaskRequests;
using Housemate.Contracts.Responses.HousingTaskResponses;

namespace Housemate.HousingTasksApi.Mapping;

public static class ContractMapping
{
    public static HousingTask MapToHousingTask(this CreateHousingTaskRequest housingTaskRequest)
    {
        return new HousingTask
        {
            Name = housingTaskRequest.Name,
            Description = housingTaskRequest.Description,
            Priority = (TaskPriority) housingTaskRequest.Priority
        };
    }
    
    public static HousingTask MapToHousingTask(this UpdateHousingTaskRequest housingTaskRequest, string id)
    {
        return new HousingTask
        {
            Id = id,
            Name = housingTaskRequest.Name,
            Description = housingTaskRequest.Description,
            Priority = (TaskPriority) housingTaskRequest.Priority,
        };
    }
    
    public static HousingTaskResponse MapToResponse(this HousingTask housingTask)
    {
        return new HousingTaskResponse
        {
            Id = housingTask.Id,
            Name = housingTask.Name,
            Description = housingTask.Description,
            CreatedAt = housingTask.CreatedAt,
            Priority = (int) housingTask.Priority
        };
    }
}