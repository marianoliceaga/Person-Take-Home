
using MediatR;
using PersonAPI.Application.DTOs;

namespace PersonAPI.Application.Queries;

public record GetAllPeopleQuery(int PageNumber = 1, int PageSize = 50) : IRequest<PagedResult<PersonDto>>;


