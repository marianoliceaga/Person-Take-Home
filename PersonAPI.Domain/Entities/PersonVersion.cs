
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Domain.Entities;

public class PersonVersion
{
    public Guid Id { get; private set; }
    public Guid PersonId { get; private set; }
    public PersonName Name { get; private set; }
    public Gender Gender { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public Location? BirthLocation { get; private set; }
    public DateOnly? DeathDate { get; private set; }
    public Location? DeathLocation { get; private set; }
    public int Version { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PersonVersion() { } // EF Constructor

    public PersonVersion(
        Guid personId,
        PersonName name,
        Gender gender,
        DateOnly? birthDate,
        Location? birthLocation,
        DateOnly? deathDate,
        Location? deathLocation,
        int version,
        DateTime createdAt)
    {
        Id = Guid.NewGuid();
        PersonId = personId;
        Name = name;
        Gender = gender;
        BirthDate = birthDate;
        BirthLocation = birthLocation;
        DeathDate = deathDate;
        DeathLocation = deathLocation;
        Version = version;
        CreatedAt = createdAt;
    }
}


