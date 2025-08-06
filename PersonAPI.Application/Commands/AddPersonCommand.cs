
using MediatR;
using PersonAPI.Application.DTOs;

namespace PersonAPI.Application.Commands;

public record AddPersonCommand(
    string GivenName,
    string? Surname,
    string Gender) : IRequest<PersonDto>;


