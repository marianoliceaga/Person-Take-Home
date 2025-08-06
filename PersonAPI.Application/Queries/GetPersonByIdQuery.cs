
using MediatR;
using PersonAPI.Application.DTOs;

namespace PersonAPI.Application.Queries;

public record GetPersonByIdQuery(Guid PersonId) : IRequest<PersonDto?>;


