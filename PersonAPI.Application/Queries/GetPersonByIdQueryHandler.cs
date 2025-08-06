
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Interfaces;

namespace PersonAPI.Application.Queries;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto?>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPersonByIdQueryHandler> _logger;

    public GetPersonByIdQueryHandler(
        IPersonRepository repository,
        IMapper mapper,
        ILogger<GetPersonByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PersonDto?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetPersonByIdQuery for person {PersonId}", request.PersonId);

        var person = await _repository.GetByIdAsync(request.PersonId, cancellationToken);
        
        if (person == null)
        {
            _logger.LogInformation("Person with ID {PersonId} not found", request.PersonId);
            return null;
        }

        _logger.LogInformation("Successfully retrieved person {PersonId}", request.PersonId);
        return _mapper.Map<PersonDto>(person);
    }
}


