using Lam3a.Controllers;
using Lam3a.Data;
using Lam3a.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Services;

public class ClientHomeService
{
    private readonly DataContextEf _context;
    

    public ClientHomeService(DataContextEf  context)
    {
        _context = context;
    }
    //get vehicles using vehicles controller
    //get address using clientProfile

    public async Task<List<ServiceProviderDTO>> GetProvidersAsync()    {
         return await _context.ServiceProviders
            .Select(p => new ServiceProviderDTO
            {
                Id = p.UserId,
                Name = p.FirstName + " " + p.LastName,
                Rating = p.Rating,
                IsAvailable = p.Availability,
                ProjectsCount = p.ServiceRequests.Count,
                AveragePrice = p.Services.Any() ? p.Services.Average(s => s.Price) : 0, // optional
                   
            })
            .OrderByDescending(p => p.ProjectsCount) // order providers by number of requests
            .ToListAsync();
    }   
    
    public async Task<List<ServiceDTO>> GetPopulerServicesAsync()
    {
         return await _context.ServiceCategories
            .OrderByDescending(c => c.Services.SelectMany(s => s.ServiceRequests).Count())
            .Select(c => new ServiceDTO
            {
                CategoryId = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
            
    }

    
    //get his vehicles
    //get his address
    //get populer servics (most services ordered in service requests) that providers give(
    //get populer service provider (most providers that have service request)
}