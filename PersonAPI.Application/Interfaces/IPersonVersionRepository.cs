
using PersonAPI.Domain.Entities;

namespace PersonAPI.Application.Interfaces;

public interface IPersonVersionRepository
{
    Task<List<PersonVersion>> GetVersionsAsync(Guid personId, CancellationToken cancellationToken = default);
    Task AddAsync(PersonVersion version, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}


