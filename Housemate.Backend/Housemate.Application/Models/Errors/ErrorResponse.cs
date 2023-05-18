namespace Housemate.Application.Models.Errors;

public sealed class ErrorResponse
{
    public required ICollection<ErrorModel> Errors { get; init; }
}