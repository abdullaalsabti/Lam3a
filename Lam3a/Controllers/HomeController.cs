using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Lam3a.Dto;
using Lam3a.Filters;
using Lam3a.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Lam3a.Controllers;
[ApiController]
[Route("api/client/[controller]")]
[Produces("application/json")]
[TypeFilter(typeof(ClientAuthorizeAttribute))]
public class HomeController : ControllerBase
{
    private readonly DataContextEf _context;
    private ClientHomeService _homeService;
    private VehicleService _vehicleService;
    public HomeController(DataContextEf context , ClientHomeService homeService , VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        _homeService = homeService;
        _context = context;
    }
    
    private AddressDto GetAddress(Client clientEntity)
    {
        return new AddressDto
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
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles()
    {
        var clientEntity = HttpContext.Items["Client"] as Client;
        try
        {
            var populerProviders = await _homeService.GetProvidersAsync();
            var populerServices = await _homeService.GetPopulerServicesAsync();
            var clientVehicles = await _vehicleService.GetVehiclesAsync(clientEntity);

            ClientHomeDTO clientHomeDTO = new ClientHomeDTO
            {
                Services = populerServices,
                Vehicles = clientVehicles,
                ServiceProviders = populerProviders,
                Address = GetAddress(clientEntity)
            };

            return Ok(clientHomeDTO);
        }
        catch(Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An error occurred while getting the vehicle." }
            );
        }
        
    }
    
    
    
    
}