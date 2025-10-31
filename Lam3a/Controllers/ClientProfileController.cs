using Lam3a.Data;
using Lam3a.Dto;
using Lam3a.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Controllers;

[ApiController]
[Authorize]
[Route("api/client/[controller]")]
[Produces("application/json")]
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
        var userId = ClaimsService.GetUserId(User);
        if (userId == null)
            return Unauthorized(new { message = "User ID not found or invalid." });

        if (!ClaimsService.IsClient(User))
            return StatusCode(
                StatusCodes.Status403Forbidden,
                new { message = "Only clients can access this endpoint." }
            );

        var clientId = userId.Value;

        var userEntity = await _context
            .Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.UserId == clientId);

        if (userEntity == null)
            return NotFound(new { message = "User not found or invalid." });

        // Update profile info
        userEntity.FirstName = clientProfileDto.FirstName;
        userEntity.LastName = clientProfileDto.LastName;
        userEntity.Gender = clientProfileDto.Gender;
        userEntity.DateOfBirth = clientProfileDto.DateOfBirth;

        // Update address
        userEntity.Address.BuildingNumber = clientProfileDto.BuildingNumber;
        userEntity.Address.Street = clientProfileDto.Street;
        userEntity.Address.Landmark = clientProfileDto.Landmark;
        userEntity.Address.MapCoordinates.Latitude = clientProfileDto.Latitude;
        userEntity.Address.MapCoordinates.Longitude = clientProfileDto.Longitude;

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
