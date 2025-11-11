using Lam3a.Data;
using Lam3a.Dto;
using Lam3a.Filters;
using Lam3a.Utils;
using Microsoft.AspNetCore.Mvc;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Controllers;

[ApiController]
[Route("api/provider/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ProviderAuthorizeAttribute))]
public class ProviderProfileController : ControllerBase
{
    private readonly DataContextEf _context;
    private ILogger<ProviderProfileController> _logger;

    public ProviderProfileController(
        DataContextEf context,
        ILogger<ProviderProfileController> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    [HttpPut("editProfile", Name = "EditProfile")]
    public async Task<IActionResult> EditProfile([FromBody] ProfileDto providerProfileDto)
    {
        var providerEntity = HttpContext.Items["provider"] as ServiceProvider;

        // Update profile info
        MapperUtil.MapEditProviderProfile(providerProfileDto, providerEntity!);

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Successfully added profile." });
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while saving the profile.", error = ex.Message }
            );
        }
    }
}
