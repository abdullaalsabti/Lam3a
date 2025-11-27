using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Dto;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Services;

public class VehicleService
{
    private readonly DataContextEf _context;
    public VehicleService(DataContextEf context)
    {
        _context = context;
    }
    
    public async Task<List<VehicleOutputDTO>> GetVehiclesAsync(Client clientEntity)
    {

        var vehicles = await _context
            .Vehicles.Include(v => v.Brand)
            .Include(v => v.Model)
            .Where(v => v.ClientId == clientEntity.UserId)
            .ToListAsync();
        
        var vehiclesDto =  vehicles
            .Select(v => new VehicleOutputDTO()
            {
                PlateNumber  = v.PlateNumber,
                Brand = v.Brand?.Name ?? "Unknown",
                Model = v.Model?.Name ?? "Unknown",
                Color = v.Color,
                CarType = v.CarType,
            }).ToList();
        
        return vehiclesDto;
    }

}