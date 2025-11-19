using Lam3a.Data;
using Lam3a.Data.ValueObjects;
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

    [HttpPut("editProfile", Name = "EditProviderProfile")]
    public async Task<IActionResult> EditProfile([FromBody] ProfileDto providerProfileDto)
    {
        var providerEntity = HttpContext.Items["Provider"] as ServiceProvider;

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

    [HttpGet("getProfile", Name = "GetProviderProfile")]
    public async Task<IActionResult> GetProfile()
    {
        var providerEntity = HttpContext.Items["Provider"] as ServiceProvider;
        var serviceProviderProfileDto = new ProfileDto
        {
            FirstName = providerEntity!.FirstName,
            LastName = providerEntity.LastName,
            Gender = providerEntity.Gender,
            DateOfBirth = providerEntity.DateOfBirth,
            Address = new AddressDto
            {
                Street = providerEntity.Address.Street,
                Landmark = providerEntity.Address.Landmark,
                BuildingNumber = providerEntity.Address.BuildingNumber,
                Coordinates = new CoordinatesDto
                {
                    Latitude = providerEntity.Address.MapCoordinates.Latitude,
                    Longitude = providerEntity.Address.MapCoordinates.Longitude,
                },
            },
        };

        return Ok(serviceProviderProfileDto);
    }

    [HttpPut("editAvailability", Name = "EditProviderAvailability")]
    public async Task<IActionResult> EditAvailability([FromBody] AvailabilityDto availabilityDto)
    {
        var providerEntity = HttpContext.Items["Provider"] as ServiceProvider;

        _context.Schedules.RemoveRange(providerEntity!.Schedules);
        foreach (var dayDto in availabilityDto.Availability)
        {
            var schedule = new Schedule
            {
                Day = dayDto.Day,
                ServiceProviderId = providerEntity.UserId,
                ServiceProvider = providerEntity,
                TimeSlots = dayDto
                    .TimeSlots.Select(slot => new TimeSlot { Start = slot.Start, End = slot.End })
                    .ToList(),
            };
            _context.Schedules.Add(schedule);
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Availability updated successfully" });
    }
}
