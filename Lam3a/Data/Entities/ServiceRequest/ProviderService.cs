namespace Lam3a.Data.Entities;

public class ProviderService
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required int EstimatedTime { get; set; }

    //Navigation
    public Guid UserId { get; set; }

    public ServiceCategory ServiceCategory { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
    public List<ServiceRequest> ServiceRequests { get; set; }
    public List<ServiceTag> ServiceTags { get; set; }
    public Guid ServiceTagId { get; set; }
}
