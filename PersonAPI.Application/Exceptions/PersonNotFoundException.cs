
namespace PersonAPI.Application.Exceptions;

public class PersonNotFoundException : Exception
{
    public PersonNotFoundException(Guid personId) 
        : base($"Person with ID {personId} was not found")
    {
        PersonId = personId;
    }

    public Guid PersonId { get; }
}
