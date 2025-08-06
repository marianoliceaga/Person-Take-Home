
using Microsoft.EntityFrameworkCore;
using PersonAPI.Application.Interfaces;
using PersonAPI.Domain.Entities;
using PersonAPI.Infrastructure.Data;

namespace PersonAPI.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonDbContext _context;

    public PersonRepository(PersonDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.People
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<(List<Person> People, int TotalCount)> GetAllAsync(
        int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.People.AsQueryable();
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var people = await query
            .OrderBy(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (people, totalCount);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        await _context.People.AddAsync(person, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
