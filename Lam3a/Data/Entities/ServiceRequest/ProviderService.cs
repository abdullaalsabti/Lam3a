using System.Text.Json.Serialization;
using Lam3a.Data.Entities;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

public class ProviderService
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int EstimatedTime { get; set; }

    // FK
    public Guid UserId { get; set; }          // FK → ServiceProvider
    public Guid CategoryId { get; set; }    // FK → ServiceTag

    // Navigation
    public ServiceProvider ServiceProvider { get; set; }
    public List<ServiceRequest> ServiceRequests { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
}