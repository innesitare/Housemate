using System.Net;
using FluentAssertions;
using Housemate.Application.Filters;
using Housemate.Application.Models.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace Housemate.Application.Tests.Filters;

public sealed class ValidationFilterTests
{
    private readonly ValidationFilter _filter;
    private readonly ActionExecutingContext _context;
    private readonly Mock<ActionExecutionDelegate> _nextDelegate;

    public ValidationFilterTests()
    {
        _filter = new ValidationFilter();
        _context = new ActionExecutingContext(
            new ActionContext 
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            },
            new List<IFilterMetadata>(),
            new Dictionary<string, object>()!,
            new Mock<Controller>().Object);

        _nextDelegate = new Mock<ActionExecutionDelegate>();
    }

    [Fact]
    public async Task OnActionExecutionAsync_ShouldCallNextDelegate_WhenModelStateIsValid()
    {
        // Arrange
        _context.ModelState.Clear();

        // Act
        await _filter.OnActionExecutionAsync(_context, _nextDelegate.Object);

        // Assert
        _nextDelegate.Verify(next => next(), Times.Once);
        _context.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task OnActionExecutionAsync_ShouldReturnBadRequestObjectResultWithErrors_WhenModelStateIsNotValid()
    {
        // Arrange
        _context.ModelState.AddModelError("field1", "error message 1");
        _context.ModelState.AddModelError("field2", "error message 2");

        // Act
        await _filter.OnActionExecutionAsync(_context, _nextDelegate.Object);

        // Assert
        _nextDelegate.Verify(next => next(), Times.Never);
        _context.Result.Should().BeOfType<BadRequestObjectResult>();

        var badRequestResult = _context.Result as BadRequestObjectResult;

        badRequestResult!.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
        badRequestResult.Value.Should().BeOfType<ErrorResponse>();

        var errorResponse = badRequestResult.Value as ErrorResponse;
        
        errorResponse!.Errors.Should().HaveCount(2);
        errorResponse.Errors.Should().Contain(e => e.FieldName == "field1" && e.Messages.Contains("error message 1"));
        errorResponse.Errors.Should().Contain(e => e.FieldName == "field2" && e.Messages.Contains("error message 2"));
    }
}