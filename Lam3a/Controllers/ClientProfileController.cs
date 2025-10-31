using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Dto;
using Lam3a.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lam3a.Controllers;

[ApiController]
[Authorize]
[Route("api/client/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ClientAuthorizeAttribute))]
public class ClientProfileController : ControllerBase
{
    private readonly DataContextEf _context;

    public ClientProfileController(DataContextEf context)
    {
        _context = context;
    }

    //SECTION: ADD PROFILE
    [HttpPost("addProfile", Name = "AddProfile")]
    public async Task<IActionResult> AddProfile([FromBody] ClientProfileDto clientProfileDto)
    {
        var clientEntity = HttpContext.Items["Client"] as Client;

        // Update profile info
        clientEntity.FirstName = clientProfileDto.FirstName;
        clientEntity.LastName = clientProfileDto.LastName;
        clientEntity.Gender = clientProfileDto.Gender;
        clientEntity.DateOfBirth = clientProfileDto.DateOfBirth;

        // Update address
        clientEntity.Address.BuildingNumber = clientProfileDto.BuildingNumber;
        clientEntity.Address.Street = clientProfileDto.Street;
        clientEntity.Address.Landmark = clientProfileDto.Landmark;
        clientEntity.Address.MapCoordinates.Latitude = clientProfileDto.Latitude;
        clientEntity.Address.MapCoordinates.Longitude = clientProfileDto.Longitude;

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
