
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Exceptions;
using PersonAPI.Application.Interfaces;
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Application.Commands;

public class RecordBirthCommandHandler : IRequestHandler<RecordBirthCommand, PersonDto>
{
    private readonly IPersonRepository _repository;
    private readonly IPersonVersionRepository _versionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RecordBirthCommandHandler> _logger;

    public RecordBirthCommandHandler(
        IPersonRepository repository,
        IPersonVersionRepository versionRepository,
        IMapper mapper,
        ILogger<RecordBirthCommandHandler> logger)
    {
        _repository = repository;
        _versionRepository = versionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PersonDto> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RecordBirthCommand for person {PersonId}", request.PersonId);

        var person = await _repository.GetByIdAsync(request.PersonId, cancellationToken);
        if (person == null)
        {
            _logger.LogWarning("Person with ID {PersonId} not found", request.PersonId);
            throw new PersonNotFoundException(request.PersonId);
        }

        // Store version before update
        var version = person.CreateVersion();
        await _versionRepository.AddAsync(version, cancellationToken);

        Location? birthLocation = null;
        if (!string.IsNullOrWhiteSpace(request.BirthCity))
        {
            birthLocation = new Location(request.BirthCity, request.BirthState, request.BirthCountry);
        }

        person.RecordBirth(request.BirthDate, birthLocation);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully recorded birth for person {PersonId}", request.PersonId);

        return _mapper.Map<PersonDto>(person);
    }
}


