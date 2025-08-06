
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Interfaces;
using PersonAPI.Domain.Entities;
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Application.Commands;

public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, PersonDto>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddPersonCommandHandler> _logger;

    public AddPersonCommandHandler(
        IPersonRepository repository,
        IMapper mapper,
        ILogger<AddPersonCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PersonDto> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling AddPersonCommand for {GivenName} {Surname}", 
            request.GivenName, request.Surname);

        var name = new PersonName(request.GivenName, request.Surname);
        var gender = Gender.FromString(request.Gender);
        var person = new Person(name, gender);

        await _repository.AddAsync(person, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully added person with ID {PersonId}", person.Id);

        return _mapper.Map<PersonDto>(person);
    }
}


