using Lam3a.Data;
using Lam3a.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Filters;

public class BrandModelFilter : IAsyncActionFilter
{
    private readonly DataContextEf _dbContext;

    public BrandModelFilter(DataContextEf dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("vehicleDto", out var obj) || obj is not VehicleDTO vehicleDto)
        {
            context.Result = new BadRequestObjectResult(new {error =  "Vehicle data is missing or invalid."});
            return;
        }

        int brandId = vehicleDto.BrandId;
        int modelId = vehicleDto.ModelId;


        var brand = await _dbContext.VehicleBrands
            .Include(b => b.Models)
            .FirstOrDefaultAsync(m => m.Id == brandId);
        
        if (brand == null)
        {
            context.Result = new BadRequestObjectResult(new { error = "vehicle brand does not exist."});
            return;
        }
        
        var model = await _dbContext.VehicleModels.FirstOrDefaultAsync(m => m.Id == modelId);

        if (!brand.Models.Contains(model))
        {
            context.Result = new BadRequestObjectResult(new { error = "The model does not belong to the specified brand."});
            return;
        }

        await next();
    }
}