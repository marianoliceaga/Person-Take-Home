
namespace PersonAPI.Domain.ValueObjects;

public class Location : IEquatable<Location>
{
    public string City { get; }
    public string? State { get; }
    public string? Country { get; }
    public string FormattedLocation => FormatLocation();

    public Location(string city, string? state = null, string? country = null)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty", nameof(city));

        City = city.Trim();
        State = string.IsNullOrWhiteSpace(state) ? null : state.Trim();
        Country = string.IsNullOrWhiteSpace(country) ? null : country.Trim();
    }

    private string FormatLocation()
    {
        var parts = new List<string> { City };
        if (!string.IsNullOrEmpty(State)) parts.Add(State);
        if (!string.IsNullOrEmpty(Country)) parts.Add(Country);
        return string.Join(", ", parts);
    }

    public bool Equals(Location? other)
    {
        if (other is null) return false;
        return City == other.City && State == other.State && Country == other.Country;
    }

    public override bool Equals(object? obj) => Equals(obj as Location);
    public override int GetHashCode() => HashCode.Combine(City, State, Country);
    public override string ToString() => FormattedLocation;

    public static bool operator ==(Location? left, Location? right) => 
        left?.Equals(right) ?? right is null;
    public static bool operator !=(Location? left, Location? right) => !(left == right);
}
