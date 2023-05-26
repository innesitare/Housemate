using FluentValidation;
using Housemate.Contracts.Responses.StudentResponses;

namespace Housemate.Application.Validaton.StudentValidaton;

public sealed class StudentResponseValidator : AbstractValidator<StudentResponse>
{
    public StudentResponseValidator()
    {
        RuleFor(response => response.Id)
            .NotEmpty();
        
        RuleFor(response => response.Name)
            .NotEmpty();
        
        RuleFor(response => response.LastName)
            .NotEmpty();

        RuleFor(response => response.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(response => response.Birthdate)
            .NotEmpty()
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
        
        RuleForEach(response => response.HousingTasks)
            .NotEmpty();
    }
}