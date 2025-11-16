using Lam3a.Data;
using Lam3a.Data.Entities;
using Lam3a.Dto;
using Lam3a.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Controllers;

[Route("api/provider/[controller]")]
[ApiController]
[TypeFilter(typeof(ProviderAuthorizeAttribute))]
public class ServicesController : ControllerBase
{
    private readonly DataContextEf _context;

    public ServicesController(DataContextEf context)
    {
        _context = context;
    }

    // -------------------- GET Categories --------------------
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _context.ServiceCategories.ToListAsync();
            var result  = categories.Select(category => new
            {
                id = category.CategoryId,
                name = category.CategoryName
            });
            
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    // -------------------- GET SERVICES --------------------
    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        try
        {
            var provider = HttpContext.Items["Provider"] as ServiceProvider;

            var services = await _context.ProviderServices
                .Include(s => s.ServiceCategory)
                .Where(s => s.UserId == provider.UserId)
                .ToListAsync();

            var result = services.Select(service => new
            {
                id = service.Id,
                categoryId = service.ServiceCategory.CategoryId,
                description = service.Description,
                price = service.Price,
                category = service.ServiceCategory.CategoryName,
                estimatedTime = service.EstimatedTime
            });

            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    // -------------------- ADD SERVICE --------------------
    [HttpPost]
    public async Task<IActionResult> PostService([FromBody] ProviderServiceDTO serviceDTO)
    {
        var provider = HttpContext.Items["Provider"] as ServiceProvider;

        try
        {
            var category = await _context.ServiceCategories.FindAsync(serviceDTO.CategoryId);
            
            if(category == null)
                return NotFound(new { error = "Category not found" });
            
            var serviceEntity = new ProviderService
            {
                UserId = provider.UserId,
                CategoryId = serviceDTO.CategoryId,
                Price = serviceDTO.Price,
                Description = serviceDTO.Description,
                EstimatedTime = serviceDTO.EstimatedTime,
            };

            await _context.ProviderServices.AddAsync(serviceEntity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "service added successfully" });
        }
        catch
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    // -------------------- EDIT SERVICE --------------------
    [HttpPut("{serviceId}")]
    public async Task<IActionResult> EditService([FromRoute] Guid serviceId,[FromBody] ProviderServiceDTO serviceDTO )
    {
        try
        {
            var provider = HttpContext.Items["Provider"] as ServiceProvider;

            var service = await _context.ProviderServices.FindAsync(serviceId);
            var category = await _context.ServiceCategories.FindAsync(serviceDTO.CategoryId);

            if (service == null)
                return NotFound(new { error = "Service not found" });
            
           if(category == null)
               return NotFound(new { error = "Category not found" });

            if (service.UserId != provider.UserId)
                return Unauthorized(new { error = "You cannot update this service" });

            var tagExists = await _context.ServiceCategories.AnyAsync(category => category.CategoryId == serviceDTO.CategoryId);
            if (!tagExists)
                return BadRequest(new { error = "Invalid ServiceTagId" });
            
            
            service.UserId = provider.UserId;
            service.CategoryId = serviceDTO.CategoryId;
            service.Price = serviceDTO.Price;
            service.Description = serviceDTO.Description;
            service.EstimatedTime = serviceDTO.EstimatedTime;

            await _context.SaveChangesAsync();

            return Ok(new { message = "service updated successfully" });
        }
        catch
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    // -------------------- DELETE SERVICE --------------------
    [HttpDelete("{serviceId}")]
    public async Task<IActionResult> DeleteService(Guid serviceId)
    {
        try
        {
            var provider = HttpContext.Items["Provider"] as ServiceProvider;

            var service = await _context.ProviderServices.FindAsync(serviceId);

            if (service == null)
                return NotFound(new { error = "Service not found" });

            if (service.UserId != provider.UserId)
                return Unauthorized(new { error = "You cannot delete this service" });

            _context.ProviderServices.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(new { message = "service deleted successfully" });
        }
        catch
        {
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
