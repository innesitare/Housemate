using Housemate.Application.Helpers;
using Housemate.Application.Services.Abstractions;
using Housemate.Contracts.Requests.HousingTaskRequests;
using Housemate.HousingTasksApi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Housemate.HousingTasksApi.Controllers;

[ApiController]
[Authorize(Policy = "Bearer")]
public sealed class HousingTasksController : ControllerBase
{
    private readonly IHousingTaskService _housingTaskService;

    public HousingTasksController(IHousingTaskService housingTaskService)
    {
        _housingTaskService = housingTaskService;
    }
    
    [HttpGet(ApiEndpoints.HousingTasks.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var housingTask = await _housingTaskService.GetByIdAsync(id.ToString(), cancellationToken);

        return housingTask is not null
            ? Ok(housingTask.MapToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.HousingTasks.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var housingTasks = await _housingTaskService.GetAllAsync(cancellationToken);
        var responses = housingTasks.Select(s => s.MapToResponse());

        return Ok(responses);
    }

    [HttpPost(ApiEndpoints.HousingTasks.Create)]
    public async Task<IActionResult> Create([FromBody] CreateHousingTaskRequest request, CancellationToken cancellationToken)
    {
        var housingTask = request.MapToHousingTask();
        bool created = await _housingTaskService.CreateAsync(housingTask, cancellationToken);

        return created
            ? CreatedAtAction(nameof(Get), new {id = housingTask.Id}, housingTask.MapToResponse())
            : BadRequest();
    }


    [HttpPut(ApiEndpoints.HousingTasks.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHousingTaskRequest request,
        CancellationToken cancellationToken)
    {
        var housingTask = request.MapToHousingTask(id.ToString());
        var updated = await _housingTaskService.UpdateAsync(id.ToString(), housingTask, cancellationToken);

        return updated is not null
            ? Ok(housingTask.MapToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.HousingTasks.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool deleted = await _housingTaskService.DeleteByIdAsync(id.ToString(), cancellationToken);

        return deleted
            ? Ok()
            : NotFound();
    }
}