
namespace PersonAPI.Domain.ValueObjects;

public class PersonName : IEquatable<PersonName>
{
    public string GivenName { get; }
    public string? Surname { get; }
    public string FullName => string.IsNullOrWhiteSpace(Surname) 
        ? GivenName 
        : $"{GivenName} {Surname}";

    public PersonName(string givenName, string? surname = null)
    {
        if (string.IsNullOrWhiteSpace(givenName))
            throw new ArgumentException("Given name cannot be null or empty", nameof(givenName));

        GivenName = givenName.Trim();
        Surname = string.IsNullOrWhiteSpace(surname) ? null : surname.Trim();
    }

    public bool Equals(PersonName? other)
    {
        if (other is null) return false;
        return GivenName == other.GivenName && Surname == other.Surname;
    }

    public override bool Equals(object? obj) => Equals(obj as PersonName);
    public override int GetHashCode() => HashCode.Combine(GivenName, Surname);
    public override string ToString() => FullName;

    public static bool operator ==(PersonName? left, PersonName? right) => 
        left?.Equals(right) ?? right is null;
    public static bool operator !=(PersonName? left, PersonName? right) => !(left == right);
}


