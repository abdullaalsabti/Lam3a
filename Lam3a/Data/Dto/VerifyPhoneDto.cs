using System.Text.RegularExpressions;
using FluentValidation;

namespace Lam3a.Dto;

public class SendOtpDto
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class SendOtpDtoValidator : AbstractValidator<SendOtpDto>
{
    public SendOtpDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Must(BeValidJordanianNumber)
            .WithMessage(
                "Invalid Jordanian mobile number must start with +962 7 and contain 9 digits after the prefix."
            );
    }

    private bool BeValidJordanianNumber(string? phone)
    {
        if (phone == null || phone.Trim().Length == 0)
            return false;

        //regex that should check if it's a valid Jordanian Number.
        var pattern = @"^\+9627\d{8}$";
        return Regex.IsMatch(phone, pattern);
    }
}
