namespace Lam3a.Data.Entities;

public class Service
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required int EstimatedTime { get; set; }

    public Guid UserId { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
    public List<ServiceRequest> ServiceRequests { get; set; }
}
