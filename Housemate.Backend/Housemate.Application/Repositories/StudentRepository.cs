using Housemate.Application.Context;
using Housemate.Application.Models.StudentInfo;
using Housemate.Application.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Housemate.Application.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _dbContext.Students.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<Student>();
        }

        return _dbContext.Students;
    }

    public async Task<Student?> GetByIdAsync(string studentId, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.FindAsync(new object?[] {studentId}, cancellationToken);

        return student;
    }
    
    public async Task<Student?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);

        return student;
    }

    public async Task<bool> CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Students.AddAsync(student, cancellationToken);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<Student?> UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        bool isContains = await _dbContext.Students.ContainsAsync(student, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return student;
    }

    public async Task<bool> DeleteByIdAsync(string studentId, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.FindAsync(new object?[] {studentId}, cancellationToken);
        if (student is null)
        {
            return false;
        }

        _dbContext.Students.Remove(student);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}