
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Interfaces;

namespace PersonAPI.Application.Queries;

public class GetPersonVersionHistoryQueryHandler : IRequestHandler<GetPersonVersionHistoryQuery, List<PersonVersionDto>>
{
    private readonly IPersonVersionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPersonVersionHistoryQueryHandler> _logger;

    public GetPersonVersionHistoryQueryHandler(
        IPersonVersionRepository repository,
        IMapper mapper,
        ILogger<GetPersonVersionHistoryQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<PersonVersionDto>> Handle(GetPersonVersionHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetPersonVersionHistoryQuery for person {PersonId}", request.PersonId);

        var versions = await _repository.GetVersionsAsync(request.PersonId, cancellationToken);

        _logger.LogInformation("Successfully retrieved {Count} versions for person {PersonId}", 
            versions.Count, request.PersonId);

        return _mapper.Map<List<PersonVersionDto>>(versions);
    }
}


