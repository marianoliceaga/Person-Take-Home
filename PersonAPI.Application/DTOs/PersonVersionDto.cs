
namespace PersonAPI.Application.DTOs;

public class PersonVersionDto
{
    public Guid PersonId { get; set; }
    public string GivenName { get; set; } = string.Empty;
    public string? Surname { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public LocationDto? BirthLocation { get; set; }
    public DateOnly? DeathDate { get; set; }
    public LocationDto? DeathLocation { get; set; }
    public int Version { get; set; }
    public DateTime CreatedAt { get; set; }
}


