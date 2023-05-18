using Housemate.Application.Models.HousingTasks;

namespace Housemate.Application.Models.StudentInfo;

public sealed class Student
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required string Password { get; init; }
    
    public required DateOnly Birthdate { get; init; }
    
    public IEnumerable<HousingTask>? HousingTasks { get; init; }
}