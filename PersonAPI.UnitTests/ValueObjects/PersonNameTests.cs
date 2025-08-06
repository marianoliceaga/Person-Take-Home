
using FluentAssertions;
using PersonAPI.Domain.ValueObjects;
using Xunit;

namespace PersonAPI.UnitTests.ValueObjects;

public class PersonNameTests
{
    [Fact]
    public void PersonName_Should_Create_With_Given_Name_Only()
    {
        // Arrange & Act
        var name = new PersonName("John");

        // Assert
        name.GivenName.Should().Be("John");
        name.Surname.Should().BeNull();
        name.FullName.Should().Be("John");
    }

    [Fact]
    public void PersonName_Should_Create_With_Full_Name()
    {
        // Arrange & Act
        var name = new PersonName("John", "Doe");

        // Assert
        name.GivenName.Should().Be("John");
        name.Surname.Should().Be("Doe");
        name.FullName.Should().Be("John Doe");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void PersonName_Should_Throw_For_Invalid_Given_Name(string givenName)
    {
        // Act & Assert
        var action = () => new PersonName(givenName);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Given name cannot be null or empty*");
    }

    [Fact]
    public void PersonName_Should_Trim_Whitespace()
    {
        // Arrange & Act
        var name = new PersonName("  John  ", "  Doe  ");

        // Assert
        name.GivenName.Should().Be("John");
        name.Surname.Should().Be("Doe");
    }

    [Fact]
    public void PersonName_Equality_Should_Work_Correctly()
    {
        // Arrange
        var name1 = new PersonName("John", "Doe");
        var name2 = new PersonName("John", "Doe");
        var name3 = new PersonName("Jane", "Doe");

        // Assert
        name1.Should().Be(name2);
        name1.Should().NotBe(name3);
        (name1 == name2).Should().BeTrue();
        (name1 != name3).Should().BeTrue();
    }
}


