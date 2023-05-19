using Housemate.Application.Models.HousingTasks;
using Housemate.Application.Models.Identity;
using Housemate.Application.Models.StudentInfo;
using Housemate.Contracts.Requests.AuthRequests;
using Housemate.Contracts.Requests.HousingTaskRequests;
using Housemate.Contracts.Requests.StudentRequests;
using Housemate.Contracts.Responses.StudentResponses;

namespace Housemate.IdentityApi.Mapping;

public static class ContractMapping
{
    private static HousingTask MapToHousingTask(this CreateHousingTaskRequest request, string studentId)
    {
        return new HousingTask
        {
            Name = request.Name,
            Description = request.Description,
            Priority = (TaskPriority) request.Priority
        };
    }

    public static StudentResponse MapToResponse(this Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            Name = student.Name,
            LastName = student.LastName,
            Birthdate = student.Birthdate,
            Email = student.Email,
            Password = student.Password
        };
    }
    
    public static Student MapToStudent(this CreateStudentRequest request)
    {
        string studentId = Guid.NewGuid().ToString();

        return new Student
        {
            Id = studentId,
            Name = request.Name,
            LastName = request.LastName,
            Birthdate = request.Birthdate,
            Email = request.Email,
            Password = request.Password,
            HousingTasks = request.HousingTasks?.Select(r => r.MapToHousingTask(studentId)),
        };
    }

    public static Student MapToStudent(this UpdateStudentRequest request, string studentId)
    {
        return new Student
        {
            Id = studentId,
            Email = request.Email,
            Name = request.Name,
            LastName = request.LastName,
            Birthdate = request.Birthdate,
            Password = request.Password,
            HousingTasks = request.HousingTasks?.Select(r => r.MapToHousingTask(studentId)),
        };
    }
    
    public static RegisterViewModel MapToViewModel(this RegisterRequest request, Role role)
    {
        return new RegisterViewModel
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            Birthdate = request.Birthdate,
            Role = role,
        };
    }

    public static LoginViewModel MapToViewModel(this LoginRequest request)
    {
        return new LoginViewModel
        {
            Email = request.Email,
            Password = request.Password,
        };
    }
}