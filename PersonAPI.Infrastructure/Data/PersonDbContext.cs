
using Microsoft.EntityFrameworkCore;
using PersonAPI.Domain.Entities;
using PersonAPI.Infrastructure.Data.Configurations;

namespace PersonAPI.Infrastructure.Data;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<PersonVersion> PersonVersions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new PersonVersionConfiguration());
    }
}


