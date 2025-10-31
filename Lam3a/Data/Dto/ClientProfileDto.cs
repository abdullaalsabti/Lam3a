using FluentValidation;
using Lam3a.Utils;

namespace Lam3a.Dto;

public class ClientProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }

    //address related:
    public string Street { get; set; }
    public string BuildingNumber { get; set; }
    public string Landmark { get; set; } = string.Empty;

    //coordinates related:
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class ClientProfileDtoValidator : AbstractValidator<ClientProfileDto>
{
    public ClientProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name cannot exceed 50 characters.");

        // Gender validation
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Invalid gender value.");

        // Date of Birth validation
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow)
            .WithMessage("Date of birth cannot be in the future.")
            .GreaterThan(DateTime.UtcNow.AddYears(-120))
            .WithMessage("Date of birth is unrealistic.");

        // Address validation
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required.")
            .MaximumLength(100)
            .WithMessage("Street cannot exceed 100 characters.");

        RuleFor(x => x.BuildingNumber)
            .NotEmpty()
            .WithMessage("Building number is required.")
            .MaximumLength(5)
            .WithMessage("Building number cannot exceed 5 characters.");

        RuleFor(x => x.Landmark)
            .MaximumLength(100)
            .WithMessage("Landmark cannot exceed 100 characters.");

        // Coordinates validation
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");
    }
}
