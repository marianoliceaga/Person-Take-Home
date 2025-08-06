
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Domain.Entities;

public class Person
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; }
    public Gender Gender { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public Location? BirthLocation { get; private set; }
    public DateOnly? DeathDate { get; private set; }
    public Location? DeathLocation { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public int Version { get; private set; }

    private Person() { } // EF Constructor

    public Person(PersonName name, Gender gender)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Version = 1;
    }

    public void RecordBirth(DateOnly? birthDate, Location? birthLocation)
    {
        if (birthDate == null && birthLocation == null)
            throw new ArgumentException("Either birth date or birth location must be provided");

        BirthDate = birthDate;
        BirthLocation = birthLocation;
        UpdatedAt = DateTime.UtcNow;
        Version++;
    }

    public void RecordDeath(DateOnly deathDate, Location? deathLocation)
    {
        if (BirthDate.HasValue && deathDate < BirthDate.Value)
            throw new ArgumentException("Death date cannot be before birth date");

        DeathDate = deathDate;
        DeathLocation = deathLocation;
        UpdatedAt = DateTime.UtcNow;
        Version++;
    }

    public PersonVersion CreateVersion()
    {
        return new PersonVersion(
            Id,
            Name,
            Gender,
            BirthDate,
            BirthLocation,
            DeathDate,
            DeathLocation,
            Version,
            UpdatedAt);
    }
}


