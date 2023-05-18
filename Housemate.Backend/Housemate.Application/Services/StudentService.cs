using Housemate.Application.Models.StudentInfo;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Services;

public sealed class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _studentRepository.GetAllAsync(cancellationToken);
    }

    public Task<Student?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _studentRepository.GetByIdAsync(id, cancellationToken);
    }
    
    public Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _studentRepository.GetByEmailAsync(email, cancellationToken);
    }

    public Task<bool> CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        return _studentRepository.CreateAsync(student, cancellationToken);
    }

    public Task<Student?> UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        return _studentRepository.UpdateAsync(student, cancellationToken);
    }

    public Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _studentRepository.DeleteByIdAsync(id, cancellationToken);
    }
}