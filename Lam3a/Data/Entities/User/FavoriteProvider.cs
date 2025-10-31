namespace Lam3a.Data.Entities;

public class FavoriteProvider
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Guid ServiceProviderId { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
}
