namespace Lam3a.Data.Entities;

public class ServiceCategory
{
    public Guid CategoryId { get; set; } = Guid.NewGuid();
    public string CategoryName { get; set; }

    //Navigation
    public List<ProviderService> Services { get; set; }
}
