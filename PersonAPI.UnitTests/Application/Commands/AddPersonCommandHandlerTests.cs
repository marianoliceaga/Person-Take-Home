
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PersonAPI.Application.Commands;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Interfaces;
using PersonAPI.Application.Mapping;
using PersonAPI.Domain.Entities;
using Xunit;

namespace PersonAPI.UnitTests.Application.Commands;

public class AddPersonCommandHandlerTests
{
    private readonly Mock<IPersonRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<AddPersonCommandHandler>> _mockLogger;
    private readonly AddPersonCommandHandler _handler;
    private readonly ILoggerFactory loggerFactory = new LoggerFactory();

    public AddPersonCommandHandlerTests()
    {
        _mockRepository = new Mock<IPersonRepository>();
        _mockLogger = new Mock<ILogger<AddPersonCommandHandler>>();
        
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PersonProfile>(), loggerFactory);
        _mapper = config.CreateMapper();
        
        _handler = new AddPersonCommandHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Person_And_Return_Dto()
    {
        // Arrange
        var command = new AddPersonCommand("John", "Doe", "Male");
        
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mockRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.GivenName.Should().Be("John");
        result.Surname.Should().Be("Doe");
        result.Gender.Should().Be("Male");
        
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData("", "Doe", "Male")]
    [InlineData(null, "Doe", "Male")]
    [InlineData("John", "Doe", "")]
    [InlineData("John", "Doe", null)]
    [InlineData("John", "Doe", "InvalidGender")]
    public async Task Handle_Should_Throw_When_Invalid_Input(string givenName, string surname, string gender)
    {
        // Arrange
        var command = new AddPersonCommand(givenName, surname, gender);

        // Act & Assert
        var action = async () => await _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<Exception>();
    }
}


