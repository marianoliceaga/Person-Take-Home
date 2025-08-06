
using FluentValidation;
using PersonAPI.Application.Commands;

namespace PersonAPI.Application.Validators;

public class RecordBirthCommandValidator : AbstractValidator<RecordBirthCommand>
{
    public RecordBirthCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .WithMessage("Person ID is required");

        RuleFor(x => x)
            .Must(x => x.BirthDate.HasValue || !string.IsNullOrWhiteSpace(x.BirthCity))
            .WithMessage("Either birth date or birth location (city) must be provided");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Birth date cannot be in the future")
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.BirthCity)
            .MaximumLength(100)
            .WithMessage("Birth city cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.BirthCity));

        RuleFor(x => x.BirthState)
            .MaximumLength(100)
            .WithMessage("Birth state cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.BirthState));

        RuleFor(x => x.BirthCountry)
            .MaximumLength(100)
            .WithMessage("Birth country cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.BirthCountry));
    }
}
