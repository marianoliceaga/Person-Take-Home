
using MediatR;
using PersonAPI.Application.DTOs;

namespace PersonAPI.Application.Queries;

public record GetPersonVersionHistoryQuery(Guid PersonId) : IRequest<List<PersonVersionDto>>;


