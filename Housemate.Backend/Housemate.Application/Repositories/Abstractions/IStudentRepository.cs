using Housemate.Application.Models.StudentInfo;

namespace Housemate.Application.Repositories.Abstractions;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}