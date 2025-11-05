using FluentValidation;
using Lam3a.Utils;

namespace Lam3a.Dto;

public class VehicleDTO
{
    public String PlateNumber { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public VehicleColor Color { get; set; }
    public CarType CarType { get; set; }
    
}

class VehicleDTOValidator : AbstractValidator<VehicleDTO>
{
    public VehicleDTOValidator()
    {
        // PlateNumber
        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("Plate number is required.")
            .MinimumLength(2).WithMessage("Plate number legnth must be greater than 2.")
            .MaximumLength(7).WithMessage("Plate number legnth must be less than 7.");


        //  Brand
        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("Brand is required.");

        //  Model
        RuleFor(x => x.ModelId)
            .NotEmpty().WithMessage("Model is required.");

    }
}