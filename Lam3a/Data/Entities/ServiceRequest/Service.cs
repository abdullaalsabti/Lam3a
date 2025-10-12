namespace Lam3a.Data.Entities;

public class Service
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required int EstimatedTime { get; set; }

    public Guid UserId { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
}
