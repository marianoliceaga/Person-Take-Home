
using FluentAssertions;
using PersonAPI.Domain.ValueObjects;
using Xunit;

namespace PersonAPI.UnitTests.ValueObjects;

public class LocationTests
{
    [Fact]
    public void Location_Should_Create_With_City_Only()
    {
        // Arrange & Act
        var location = new Location("New York");

        // Assert
        location.City.Should().Be("New York");
        location.State.Should().BeNull();
        location.Country.Should().BeNull();
        location.FormattedLocation.Should().Be("New York");
    }

    [Fact]
    public void Location_Should_Create_With_Full_Address()
    {
        // Arrange & Act
        var location = new Location("New York", "NY", "USA");

        // Assert
        location.City.Should().Be("New York");
        location.State.Should().Be("NY");
        location.Country.Should().Be("USA");
        location.FormattedLocation.Should().Be("New York, NY, USA");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Location_Should_Throw_For_Invalid_City(string city)
    {
        // Act & Assert
        var action = () => new Location(city);
        action.Should().Throw<ArgumentException>()
            .WithMessage("City cannot be null or empty*");
    }

    [Fact]
    public void Location_Should_Trim_Whitespace()
    {
        // Arrange & Act
        var location = new Location("  New York  ", "  NY  ", "  USA  ");

        // Assert
        location.City.Should().Be("New York");
        location.State.Should().Be("NY");
        location.Country.Should().Be("USA");
    }

    [Fact]
    public void Location_Should_Handle_Null_State_And_Country()
    {
        // Arrange & Act
        var location = new Location("London", null, "UK");

        // Assert
        location.City.Should().Be("London");
        location.State.Should().BeNull();
        location.Country.Should().Be("UK");
        location.FormattedLocation.Should().Be("London, UK");
    }

    [Fact]
    public void Location_Equality_Should_Work_Correctly()
    {
        // Arrange
        var location1 = new Location("New York", "NY", "USA");
        var location2 = new Location("New York", "NY", "USA");
        var location3 = new Location("London", null, "UK");

        // Assert
        location1.Should().Be(location2);
        location1.Should().NotBe(location3);
        (location1 == location2).Should().BeTrue();
        (location1 != location3).Should().BeTrue();
    }
}