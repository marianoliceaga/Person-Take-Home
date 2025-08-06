
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.WebApi.Models;

public class RecordBirthRequest
{
    /// <summary>
    /// The person's birth date
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// The city where the person was born
    /// </summary>
    [StringLength(100)]
    public string? BirthCity { get; set; }

    /// <summary>
    /// The state/province where the person was born
    /// </summary>
    [StringLength(100)]
    public string? BirthState { get; set; }

    /// <summary>
    /// The country where the person was born
    /// </summary>
    [StringLength(100)]
    public string? BirthCountry { get; set; }
}


