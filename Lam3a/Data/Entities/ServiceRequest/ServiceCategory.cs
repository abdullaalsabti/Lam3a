namespace Lam3a.Data.Entities;

public class ServiceCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    //Navigation
    public List<ProviderService> Services { get; set; }
}
