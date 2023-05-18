using Housemate.Contracts.Requests.HousingTaskRequests;

namespace Housemate.Contracts.Requests.StudentRequests;

public sealed class CreateStudentRequest
{
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required string Password { get; init; }
    
    public required DateOnly Birthdate { get; init; }

    public IEnumerable<CreateHousingTaskRequest>? HousingTasks { get; init; }
}