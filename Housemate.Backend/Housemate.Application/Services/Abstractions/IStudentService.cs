using Housemate.Application.Models.StudentInfo;

namespace Housemate.Application.Services.Abstractions;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Student?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
    Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task<bool> CreateAsync(Student student, CancellationToken cancellationToken = default);

    Task<Student?> UpdateAsync(Student student, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
}