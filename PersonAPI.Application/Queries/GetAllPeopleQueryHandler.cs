
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Interfaces;

namespace PersonAPI.Application.Queries;

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, PagedResult<PersonDto>>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPeopleQueryHandler> _logger;

    public GetAllPeopleQueryHandler(
        IPersonRepository repository,
        IMapper mapper,
        ILogger<GetAllPeopleQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<PersonDto>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllPeopleQuery - Page {PageNumber}, Size {PageSize}", 
            request.PageNumber, request.PageSize);

        var (people, totalCount) = await _repository.GetAllAsync(
            request.PageNumber, request.PageSize, cancellationToken);

        var peopleDto = _mapper.Map<List<PersonDto>>(people);

        _logger.LogInformation("Successfully retrieved {Count} people out of {Total}", 
            peopleDto.Count, totalCount);

        return new PagedResult<PersonDto>(
            peopleDto,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}


