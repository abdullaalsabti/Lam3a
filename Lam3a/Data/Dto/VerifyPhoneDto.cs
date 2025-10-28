using FluentValidation;

namespace Lam3a.Dto;

public class VerifyPhoneDto
{
    public Guid UserId { get; set; }
    public string FirebaseIdToken { get; set; } // Token from Flutter after OTP verification
    public string Phone { get; set; }
}

public class VerifyPhoneDtoValidator : AbstractValidator<VerifyPhoneDto>
{
    public VerifyPhoneDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.FirebaseIdToken).NotEmpty().WithMessage("Firebase ID token is required.");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+9627\d{8}$") // Jordanian mobile numbers only
            .WithMessage(
                "Phone number must be a valid Jordanian mobile number in 'E.164' format (+9627XXXXXXXX)."
            );
    }
}
