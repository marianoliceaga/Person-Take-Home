
using FluentAssertions;
using PersonAPI.Domain.Entities;
using PersonAPI.Domain.ValueObjects;
using Xunit;

namespace PersonAPI.UnitTests.Domain;

public class PersonTests
{
    [Fact]
    public void Person_Constructor_Should_Create_Valid_Person()
    {
        // Arrange
        var name = new PersonName("John", "Doe");
        var gender = Gender.Male;

        // Act
        var person = new Person(name, gender);

        // Assert
        person.Id.Should().NotBeEmpty();
        person.Name.Should().Be(name);
        person.Gender.Should().Be(gender);
        person.Version.Should().Be(1);
        person.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        person.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void RecordBirth_Should_Update_Person_And_Increment_Version()
    {
        // Arrange
        var person = new Person(new PersonName("John", "Doe"), Gender.Male);
        var birthDate = new DateOnly(1990, 5, 15);
        var birthLocation = new Location("New York", "NY", "USA");
        var originalVersion = person.Version;

        // Act
        person.RecordBirth(birthDate, birthLocation);

        // Assert
        person.BirthDate.Should().Be(birthDate);
        person.BirthLocation.Should().Be(birthLocation);
        person.Version.Should().Be(originalVersion + 1);
        person.UpdatedAt.Should().BeAfter(person.CreatedAt);
    }

    [Fact]
    public void RecordBirth_Should_Throw_When_No_Birth_Info_Provided()
    {
        // Arrange
        var person = new Person(new PersonName("John", "Doe"), Gender.Male);

        // Act & Assert
        var action = () => person.RecordBirth(null, null);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Either birth date or birth location must be provided");
    }
}


