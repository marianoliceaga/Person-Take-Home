
using FluentAssertions;
using PersonAPI.Domain.ValueObjects;
using Xunit;

namespace PersonAPI.UnitTests.ValueObjects;

public class GenderTests
{
    [Theory]
    [InlineData("Male")]
    [InlineData("Female")]
    [InlineData("Other")]
    [InlineData("PreferNotToSay")]
    public void FromString_Should_Return_Valid_Gender(string value)
    {
        // Act
        var gender = Gender.FromString(value);

        // Assert
        gender.Should().NotBeNull();
        gender.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("InvalidGender")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void FromString_Should_Throw_For_Invalid_Values(string value)
    {
        // Act & Assert
        var action = () => Gender.FromString(value);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetAll_Should_Return_All_Valid_Genders()
    {
        // Act
        var genders = Gender.GetAll().ToList();

        // Assert
        genders.Should().HaveCount(4);
        genders.Should().Contain(g => g.Value == "Male");
        genders.Should().Contain(g => g.Value == "Female");
        genders.Should().Contain(g => g.Value == "Other");
        genders.Should().Contain(g => g.Value == "PreferNotToSay");
    }

    [Fact]
    public void Gender_Equality_Should_Work_Correctly()
    {
        // Arrange
        var male1 = Gender.Male;
        var male2 = Gender.FromString("Male");
        var female = Gender.Female;

        // Assert
        male1.Should().Be(male2);
        male1.Should().NotBe(female);
        (male1 == male2).Should().BeTrue();
        (male1 != female).Should().BeTrue();
    }
}


