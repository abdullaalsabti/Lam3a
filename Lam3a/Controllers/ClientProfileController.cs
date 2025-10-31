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
    [HttpPut("editProfile", Name = "EditProfile")]
    public async Task<IActionResult> EditProfile([FromBody] ClientProfileDto clientProfileDto)
    {
        var clientEntity = HttpContext.Items["Client"] as Client;

        // Update profile info
        clientEntity!.FirstName = clientProfileDto.FirstName;
        clientEntity.LastName = clientProfileDto.LastName;
        clientEntity.Gender = clientProfileDto.Gender;
        clientEntity.DateOfBirth = clientProfileDto.DateOfBirth;

        // Update address
        clientEntity.Address.Street = clientProfileDto.Address.Street;
        clientEntity.Address.BuildingNumber = clientProfileDto.Address.BuildingNumber;
        clientEntity.Address.Landmark = clientProfileDto.Address.Landmark;
        clientEntity.Address.MapCoordinates.Latitude = clientProfileDto
            .Address
            .Coordinates
            .Latitude;
        clientEntity.Address.MapCoordinates.Longitude = clientProfileDto
            .Address
            .Coordinates
            .Longitude;

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

    [HttpGet("getProfile", Name = "GetProfile")]
    public async Task<IActionResult> GetProfile()
    {
        var clientEntity = HttpContext.Items["Client"] as Client;
        var clientProfileDto = new ClientProfileDto
        {
            FirstName = clientEntity!.FirstName,
            LastName = clientEntity.LastName,
            Gender = clientEntity.Gender,
            DateOfBirth = clientEntity.DateOfBirth,
            Address = new AddressDto
            {
                Street = clientEntity.Address.Street,
                Landmark = clientEntity.Address.Landmark,
                BuildingNumber = clientEntity.Address.BuildingNumber,
                Coordinates = new CoordinatesDto
                {
                    Latitude = clientEntity.Address.MapCoordinates.Latitude,
                    Longitude = clientEntity.Address.MapCoordinates.Longitude,
                },
            },
        };

        return Ok(clientProfileDto);
    }

    [HttpGet("getAddress", Name = "GetAddress")]
    public IActionResult GetAddress()
    {
        var clientEntity = HttpContext.Items["Client"] as Client;

        var addressDto = new AddressDto
        {
            Street = clientEntity!.Address.Street,
            BuildingNumber = clientEntity.Address.BuildingNumber,
            Landmark = clientEntity.Address.Landmark,
            Coordinates = new CoordinatesDto
            {
                Latitude = clientEntity.Address.MapCoordinates.Latitude,
                Longitude = clientEntity.Address.MapCoordinates.Longitude,
            },
        };

        return Ok(addressDto);
    }

    [HttpPut("editAddress", Name = "EditAddress")]
    public async Task<IActionResult> EditAddress([FromBody] AddressDto addressDto)
    {
        var clientEntity = HttpContext.Items["Client"] as Client;

        clientEntity!.Address.Street = addressDto.Street;
        clientEntity.Address.BuildingNumber = addressDto.BuildingNumber;
        clientEntity.Address.Landmark = addressDto.Landmark;
        clientEntity.Address.MapCoordinates.Latitude = addressDto.Coordinates.Latitude;
        clientEntity.Address.MapCoordinates.Longitude = addressDto.Coordinates.Longitude;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Address updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new
                {
                    message = "An error occurred while updating the address.",
                    error = ex.Message,
                }
            );
        }
    }
}
