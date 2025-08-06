
namespace PersonAPI.Application.DTOs;

public class LocationDto
{
    public string City { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? Country { get; set; }
    public string FormattedLocation { get; set; } = string.Empty;
}


