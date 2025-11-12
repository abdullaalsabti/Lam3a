using Lam3a.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelBrandController : ControllerBase
{
    private readonly DataContextEf _context;

    public ModelBrandController(DataContextEf context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetBrandsAndModels()
    {
        try
        {
            var brands = await _context.VehicleBrands.Include(brand => brand.Models).ToListAsync();

            if (brands.Count == 0)
                return NoContent();

            return Ok(brands);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("brands")]
    public async Task<ActionResult> GetBrands()
    {
        try
        {
            var brands = await _context.VehicleBrands.ToListAsync();
            var brandsResposne = brands.Select(brand => new { name = brand.Name, id = brand.Id });

            if (brands.Count == 0)
                return NoContent();

            return Ok(brandsResposne);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("models")]
    public async Task<ActionResult> GetModels()
    {
        try
        {
            var models = await _context.VehicleModels.ToListAsync();
            return Ok(models);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
