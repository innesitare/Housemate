using Housemate.Application.Models.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Housemate.Application.Filters;

public sealed class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid)
        {
            await next();
        }

        var errorModels = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .Select(x => new ErrorModel
            {
                FieldName = x.Key,
                Messages = x.Value!.Errors.Select(error => error.ErrorMessage)
            });

        var response = new ErrorResponse
        {
            Errors = new List<ErrorModel>(errorModels)
        };

        context.Result = new BadRequestObjectResult(response);
    }
}