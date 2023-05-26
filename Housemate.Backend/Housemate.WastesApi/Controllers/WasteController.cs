using Housemate.Application.Helpers;
using Housemate.Application.Services.Abstractions;
using Housemate.Contracts.Requests.StudentRequests;
using Housemate.Contracts.Requests.WasteRequests;
using Housemate.WastesApi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Housemate.WastesApi.Controllers;

[ApiController]
[Authorize(Policy = "Bearer")]
public sealed class WasteController : ControllerBase
{
    private readonly IWasteService _wasteService;

    public WasteController(IWasteService wasteService)
    {
        _wasteService = wasteService;
    }

    [HttpGet(ApiEndpoints.Wastes.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var waste = await _wasteService.GetByIdAsync(id.ToString(), cancellationToken);

        return waste is not null
            ? Ok(waste.MapToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.Wastes.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var wastes = await _wasteService.GetAllAsync(cancellationToken);
        var responses = wastes.Select(w => w.MapToResponse());

        return Ok(responses);
    }

    [HttpPost(ApiEndpoints.Wastes.Create)]
    public async Task<IActionResult> Create([FromBody] CreateWasteRequest request, CancellationToken cancellationToken)
    {
        var waste = request.MapToWaste();
        bool created = await _wasteService.CreateAsync(waste, cancellationToken);

        return created
            ? CreatedAtAction(nameof(Get), new {id = waste.Id}, waste.MapToResponse())
            : BadRequest();
    }


    [HttpPut(ApiEndpoints.Wastes.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWasteRequest request,
        CancellationToken cancellationToken)
    {
        var waste = request.MapToWaste(id.ToString());
        var updated = await _wasteService.UpdateAsync(waste, cancellationToken);

        return updated is not null
            ? Ok(waste.MapToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.Wastes.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool deleted = await _wasteService.DeleteByIdAsync(id.ToString(), cancellationToken);

        return deleted
            ? Ok()
            : NotFound();
    }
}