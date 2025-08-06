
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.WebApi.Models;

public class AddPersonRequest
{
    /// <summary>
    /// The person's given name (first name)
    /// </summary>
    [Required]
    [StringLength(100)]
    public string GivenName { get; set; } = string.Empty;

    /// <summary>
    /// The person's surname (last name)
    /// </summary>
    [StringLength(100)]
    public string? Surname { get; set; }

    /// <summary>
    /// The person's gender (Male, Female, Other, PreferNotToSay)
    /// </summary>
    [Required]
    public string Gender { get; set; } = string.Empty;
}


