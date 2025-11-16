using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Dto;
using Lam3a.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Controllers;

[ApiController]
[Authorize]
[Route("api/client/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ClientAuthorizeAttribute))]
public class VehicleController : ControllerBase
{
    private readonly DataContextEf _context;
    private readonly ILogger<VehicleController> _logger;

    public VehicleController(DataContextEf context, ILogger<VehicleController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //get all vehicles
    [HttpGet("getVehicles", Name = "GetVehicles")]
    public async Task<IActionResult> GetAllVehicles()
    {
        var clientEntity = HttpContext.Items["Client"] as Client;
      
        try
        {
            var vehicles = await _context
                .Vehicles.Include(v => v.Brand)
                .Include(v => v.Model)
                .Where(v => v.ClientId == clientEntity.UserId)
                .ToListAsync();

            var vehiclesDto = vehicles
                .Select(v => new
                {
                    v.PlateNumber,
                    BrandName = v.Brand?.Name ?? "Unknown",
                    ModelName = v.Model?.Name ?? "Unknown",
                    color = v.Color.ToString(),
                    type = v.CarType.ToString(),
                })
                .ToList();

            return Ok(vehiclesDto);
        }
        catch (Exception e)
        {
            _logger.LogError("error while retrieving vehicles " + e.Message);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "An error occurred while getting vehicles."
            );
        }
    }

    //get one vehicle (id)
    [HttpGet("{plateNumber}")]
    public async Task<IActionResult> GetVehicle(string plateNumber)
    {
        try
        {
            var vehicle = await _context
                .Vehicles.Include(b => b.Brand)
                .Include(b => b.Model)
                .FirstOrDefaultAsync(v => v.PlateNumber == plateNumber);

            if (vehicle == null)
                return NotFound(new { error = "Vehicle not found." });

            return Ok(
                new
                {
                    plateNumber = vehicle.PlateNumber,
                    color = vehicle.Color.ToString(),
                    type = vehicle.CarType.ToString(),
                    brand = vehicle.Brand.Name,
                    model = vehicle.Model.Name,
                }
            );
        }
        catch (Exception e)
        {
            _logger.LogError("error while vehicle " + e.Message);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while getting the vehicle." }
            );
        }
    }

    //add a vehicle (post)
    [HttpPost("addVehicle", Name = "AddVehicle")]
    [TypeFilter(typeof(BrandModelFilter))]
    public async Task<IActionResult> PostVehicle([FromBody] VehicleDTO vehicleDto)
    {
        var clientEntity = HttpContext.Items["Client"] as Client;

        var vehicleEntity = new Vehicle(
            vehicleDto.PlateNumber,
            vehicleDto.BrandId,
            vehicleDto.ModelId,
            vehicleDto.Color,
            vehicleDto.CarType,
            clientEntity.UserId
        );
        try
        {
            if (await IsExistAsync(vehicleDto.PlateNumber))
                return BadRequest("Vehicle already exists");

            await _context.AddAsync(vehicleEntity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Successfully added vehicle" });
        }
        catch (Exception e)
        {
            _logger.LogError("error while adding vehicle " + e.Message);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while saving the vehicle." }
            );
        }
    }

    //edit a vehicle (put)
    [HttpPut("{plateNumber}")]
    [TypeFilter(typeof(BrandModelFilter))]
    public async Task<IActionResult> PutVehicle(string plateNumber, VehicleDTO vehicleDto)
    {
        try
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v =>
                v.PlateNumber == plateNumber
            );

            if (vehicle == null)
                return NotFound(new { error = "Vehicle not found." });

            vehicle.PlateNumber = vehicleDto.PlateNumber;
            vehicle.Color = vehicleDto.Color;
            vehicle.ModelId = vehicleDto.ModelId;
            vehicle.BrandId = vehicleDto.BrandId;
            vehicle.CarType = vehicleDto.CarType;

            _context.Update(vehicle);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Successfully updated vehicle" });
        }
        catch (Exception e)
        {
            _logger.LogError("error while editing vehicle " + e.Message);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while editing the vehicle." }
            );
        }
    }

    // DELETE: api/client/vehicle/{plateNumber}
    [HttpDelete("{plateNumber}")]
    public async Task<IActionResult> DeleteVehicle(string plateNumber)
    {
        try
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v =>
                v.PlateNumber == plateNumber
            );

            if (vehicle == null)
                return NotFound(new { error = "Vehicle not found." });

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Vehicle {PlateNumber} deleted.", plateNumber);
            return Ok(new { message = "Vehicle deleted successfully." });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting vehicle {PlateNumber}", plateNumber);
            _logger.LogError(e, "Error message {EMessage}", e.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while deleting the vehicle." }
            );
        }
    }

    // Async existence check
    private async Task<bool> IsExistAsync(string plateNumber)
    {
        return await _context.Vehicles.AnyAsync(v => v.PlateNumber == plateNumber);
    }
}
