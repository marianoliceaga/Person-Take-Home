
using PersonAPI.Domain.Entities;

namespace PersonAPI.Application.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(List<Person> People, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Person person, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}


