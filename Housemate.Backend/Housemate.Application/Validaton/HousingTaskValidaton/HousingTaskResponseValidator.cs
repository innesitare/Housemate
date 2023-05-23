using FluentValidation;
using Housemate.Application.Models.HousingTasks;
using Housemate.Contracts.Responses.HousingTaskResponses;

namespace Housemate.Application.Validaton.HousingTaskValidaton;

public sealed class HousingTaskResponseValidator : AbstractValidator<HousingTaskResponse>
{
    public HousingTaskResponseValidator()
    {
        RuleFor(response => response.Id)
            .NotEmpty();
            
        RuleFor(response => response.Name)
            .NotEmpty();

        RuleFor(response => response.Description)
            .NotEmpty();
        
        RuleFor(response => response.CreatedAt)
            .NotEmpty();

        RuleFor(response => response.Priority)
            .Must(state => Enum.IsDefined(typeof(TaskPriority), state))
            .WithMessage("Invalid housing task priority. Must be an inclusive value between 0 and 3");
    }
}