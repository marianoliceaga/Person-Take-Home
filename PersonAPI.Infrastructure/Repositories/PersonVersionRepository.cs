
using Microsoft.EntityFrameworkCore;
using PersonAPI.Application.Interfaces;
using PersonAPI.Domain.Entities;
using PersonAPI.Infrastructure.Data;

namespace PersonAPI.Infrastructure.Repositories;

public class PersonVersionRepository : IPersonVersionRepository
{
    private readonly PersonDbContext _context;

    public PersonVersionRepository(PersonDbContext context)
    {
        _context = context;
    }

    public async Task<List<PersonVersion>> GetVersionsAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return await _context.PersonVersions
            .Where(v => v.PersonId == personId)
            .OrderByDescending(v => v.Version)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PersonVersion version, CancellationToken cancellationToken = default)
    {
        await _context.PersonVersions.AddAsync(version, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}


