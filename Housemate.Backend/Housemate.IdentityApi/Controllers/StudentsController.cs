using Housemate.Application.Helpers;
using Housemate.Application.Services.Abstractions;
using Housemate.Contracts.Requests.StudentRequests;
using Housemate.IdentityApi.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Housemate.IdentityApi.Controllers;

[ApiController]
[Authorize(Policy = "Bearer")]
public sealed class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet(ApiEndpoints.Students.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(id.ToString(), cancellationToken);

        return student is not null
            ? Ok(student.MapToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.Students.GetByEmail)]
    public async Task<IActionResult> GetByEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByEmailAsync(email, cancellationToken);

        return student is not null
            ? Ok(student.MapToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.Students.Create)]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
    {
        var student = request.MapToStudent();
        bool created = await _studentService.CreateAsync(student, cancellationToken);

        return created
            ? CreatedAtAction(nameof(Get), new {id = student.Id}, student.MapToResponse())
            : BadRequest();
    }


    [HttpPut(ApiEndpoints.Students.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequest request,
        CancellationToken cancellationToken)
    {
        var student = request.MapToStudent(id.ToString());
        var updated = await _studentService.UpdateAsync(student, cancellationToken);

        return updated is not null
            ? Ok(student.MapToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.Students.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool deleted = await _studentService.DeleteByIdAsync(id.ToString(), cancellationToken);

        return deleted
            ? Ok()
            : NotFound();
    }
}