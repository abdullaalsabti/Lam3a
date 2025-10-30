namespace Lam3a.Data.Entities;

public class ServiceCategory
{
    public Guid CategoryId { get; set; } = Guid.NewGuid();
    public required string CategoryName { get; set; }
    public string? Description { get; set; }

    // Navigation
    public List<ProviderService> Services { get; set; } = new();
}
