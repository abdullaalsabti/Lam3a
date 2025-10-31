namespace Lam3a.Data.Entities;

public class ServiceTag
{
    public Guid TagId { get; set; } = Guid.NewGuid();
    public string TagName { get; set; }

    //Navigation
    public List<ProviderService> Services { get; set; }
}
