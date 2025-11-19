using FluentValidation;

namespace Lam3a.Dto;

public class ProviderServiceDTO
{
    public  Guid CategoryId { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required int EstimatedTime { get; set; }

}

public class ProviderServiceDTOValidator : AbstractValidator<ProviderServiceDTO>
{
    public ProviderServiceDTOValidator()
    {
        RuleFor(ps => ps.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");
        
        // price
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
        
        //  description
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        //  estimated time
        RuleFor(x => x.EstimatedTime)
            .GreaterThan(0).WithMessage("Estimated time must be greater than 0");

    }
}