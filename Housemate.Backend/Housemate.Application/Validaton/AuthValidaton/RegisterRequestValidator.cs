using FluentValidation;
using Housemate.Contracts.Requests.AuthRequests;

namespace Housemate.Application.Validaton.AuthValidaton;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(@"^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\D*\d)(?=[^!#%]*[!#%])[A-Za-z0-9!#%]{6,32}$");
        
        RuleFor(x => x.Birthdate)
            .NotEmpty()
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
    }
}