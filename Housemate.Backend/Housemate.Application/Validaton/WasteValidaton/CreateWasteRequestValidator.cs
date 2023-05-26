using FluentValidation;
using Housemate.Application.Models.Wastes;
using Housemate.Contracts.Requests.WasteRequests;

namespace Housemate.Application.Validaton.WasteValidaton;

public sealed class CreateWasteRequestValidator : AbstractValidator<CreateWasteRequest>
{
    public CreateWasteRequestValidator()
    {
        RuleFor(request => request.CollectionDay)
            .NotEmpty()
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(request => request.WasteType)
            .Must(state => Enum.IsDefined(typeof(WasteType), state))
            .WithMessage("Invalid waste type. Must be an inclusive value between 0 and 4");
    }
}