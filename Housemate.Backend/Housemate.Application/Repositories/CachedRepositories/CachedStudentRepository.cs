using Housemate.Application.Attributes;
using Housemate.Application.Helpers;
using Housemate.Application.Models.StudentInfo;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Repositories.CachedRepositories;

[CachingDecorator]
public sealed class CachedStudentRepository : IStudentRepository
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICacheService<Student> _cacheService;

    public CachedStudentRepository(IStudentRepository studentRepository, ICacheService<Student> cacheService)
    {
        _studentRepository = studentRepository;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.Student.GetAll, async () =>
        {
            var students = await _studentRepository.GetAllAsync(cancellationToken);
            return students;
        }, cancellationToken);
    }

    public Task<Student?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Student.Get(id), async () =>
        {
            var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
            return student;
        }, cancellationToken);
    }
    
    public Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Student.GetByEmail(email), async () =>
        {
            var student = await _studentRepository.GetByEmailAsync(email, cancellationToken);
            return student;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        bool created = await _studentRepository.CreateAsync(student, cancellationToken);
        if (created)
        {
            await _cacheService.RemoveAsync(CacheKeys.Student.GetAll, cancellationToken);
        }

        return created;
    }

    public async Task<Student?> UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var updated = await _studentRepository.UpdateAsync(student, cancellationToken);
        if (updated is null)
        {
            return updated;
        }

        await _cacheService.RemoveAsync(CacheKeys.Student.Get(student.Id), cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.Student.GetByEmail(student.Email), cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.Student.GetAll, cancellationToken);

        return updated;
    }

    public async Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        bool deleted = await _studentRepository.DeleteByIdAsync(id, cancellationToken);
        if (!deleted)
        {
            return deleted;
        }

        await _cacheService.RemoveAsync(CacheKeys.Student.GetAll, cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.Student.Get(id), cancellationToken);

        return deleted;
    }
}