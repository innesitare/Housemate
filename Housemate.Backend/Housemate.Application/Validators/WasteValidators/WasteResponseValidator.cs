using FluentValidation;
using Housemate.Application.Models.Wastes;
using Housemate.Contracts.Responses.WasteResponses;

namespace Housemate.Application.Validators.WasteValidators;

public sealed class WasteResponseValidator : AbstractValidator<WasteResponse>
{
    public WasteResponseValidator()
    {
        RuleFor(response => response.Id)
            .NotEmpty();
            
        RuleFor(response => response.CollectionDay)
            .NotEmpty();

        RuleFor(response => response.WasteType)
            .Must(state => Enum.IsDefined(typeof(WasteType), state))
            .WithMessage("Invalid waste type. Must be an inclusive value between 0 and 4");
    }
}