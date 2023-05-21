using FluentValidation;
using Housemate.Application.Models.HousingTasks;
using Housemate.Contracts.Requests.HousingTaskRequests;

namespace Housemate.Application.Validators.HousingTaskValidators;

public sealed class CreateHousingTaskRequestValidator : AbstractValidator<CreateHousingTaskRequest>
{
    public CreateHousingTaskRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty();
            
        RuleFor(request => request.Description)
            .NotEmpty();

        RuleFor(request => request.Priority)
            .Must(state => Enum.IsDefined(typeof(TaskPriority), state))
            .WithMessage("Invalid housing task priority. Must be an inclusive value between 0 and 3");
    }
}