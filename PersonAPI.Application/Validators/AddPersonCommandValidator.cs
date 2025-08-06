
using FluentValidation;
using PersonAPI.Application.Commands;
using PersonAPI.Domain.ValueObjects;

namespace PersonAPI.Application.Validators;

public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
    public AddPersonCommandValidator()
    {
        RuleFor(x => x.GivenName)
            .NotEmpty()
            .WithMessage("Given name is required")
            .MaximumLength(100)
            .WithMessage("Given name cannot exceed 100 characters");

        RuleFor(x => x.Surname)
            .MaximumLength(100)
            .WithMessage("Surname cannot exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.Surname));

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("Gender is required")
            .Must(BeValidGender)
            .WithMessage($"Gender must be one of: {string.Join(", ", Gender.GetAll().Select(g => g.Value))}");
    }

    private static bool BeValidGender(string gender)
    {
        try
        {
            Gender.FromString(gender);
            return true;
        }
        catch
        {
            return false;
        }
    }
}


