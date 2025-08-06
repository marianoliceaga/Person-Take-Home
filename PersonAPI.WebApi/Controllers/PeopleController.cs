
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonAPI.Application.Commands;
using PersonAPI.Application.DTOs;
using PersonAPI.Application.Queries;
using PersonAPI.WebApi.Models;

namespace PersonAPI.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(IMediator mediator, ILogger<PeopleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all people with pagination
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 50, max: 100)</param>
    /// <returns>Paginated list of people</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<PersonDto>>> GetAllPeople(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1 || pageSize > 100) pageSize = 50;

        var query = new GetAllPeopleQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    /// <summary>
    /// Get a person by ID
    /// </summary>
    /// <param name="id">Person ID</param>
    /// <returns>Person details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonDto>> GetPersonById(Guid id)
    {
        var query = new GetPersonByIdQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound($"Person with ID {id} not found");
        }
        
        return Ok(result);
    }

    /// <summary>
    /// Add a new person
    /// </summary>
    /// <param name="request">Person creation request</param>
    /// <returns>Created person</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonDto>> AddPerson([FromBody] AddPersonRequest request)
    {
        var command = new AddPersonCommand(request.GivenName, request.Surname, request.Gender);
        var result = await _mediator.Send(command);
        
        return CreatedAtAction(
            nameof(GetPersonById), 
            new { id = result.Id }, 
            result);
    }

    /// <summary>
    /// Record birth information for a person
    /// </summary>
    /// <param name="id">Person ID</param>
    /// <param name="request">Birth information request</param>
    /// <returns>Updated person</returns>
    [HttpPut("{id:guid}/birth")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonDto>> RecordBirth(Guid id, [FromBody] RecordBirthRequest request)
    {
        var command = new RecordBirthCommand(
            id, 
            request.BirthDate, 
            request.BirthCity, 
            request.BirthState, 
            request.BirthCountry);
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get version history for a person
    /// </summary>
    /// <param name="id">Person ID</param>
    /// <returns>List of person versions</returns>
    [HttpGet("{id:guid}/versions")]
    [ProducesResponseType(typeof(List<PersonVersionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<PersonVersionDto>>> GetPersonVersionHistory(Guid id)
    {
        var query = new GetPersonVersionHistoryQuery(id);
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }
}


