
using MediatR;
using PersonAPI.Application.DTOs;

namespace PersonAPI.Application.Commands;

public record RecordBirthCommand(
    Guid PersonId,
    DateOnly? BirthDate,
    string? BirthCity,
    string? BirthState,
    string? BirthCountry) : IRequest<PersonDto>;


