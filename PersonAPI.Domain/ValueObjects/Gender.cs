
namespace PersonAPI.Domain.ValueObjects;

public class Gender : IEquatable<Gender>
{
    public static readonly Gender Male = new("Male");
    public static readonly Gender Female = new("Female");
    public static readonly Gender Other = new("Other");
    public static readonly Gender PreferNotToSay = new("PreferNotToSay");

    private static readonly Dictionary<string, Gender> _values = new()
    {
        { Male.Value, Male },
        { Female.Value, Female },
        { Other.Value, Other },
        { PreferNotToSay.Value, PreferNotToSay }
    };

    public string Value { get; }

    private Gender(string value)
    {
        Value = value;
    }

    public static Gender FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Gender value cannot be null or empty");

        if (_values.TryGetValue(value, out var gender))
            return gender;

        throw new ArgumentException($"Invalid gender value: {value}");
    }

    public static IEnumerable<Gender> GetAll() => _values.Values;

    public bool Equals(Gender? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => Equals(obj as Gender);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(Gender? left, Gender? right) => 
        left?.Equals(right) ?? right is null;
    public static bool operator !=(Gender? left, Gender? right) => !(left == right);
}


