using Lam3a.Data.ValueObjects;

namespace Lam3a.Data.Entities;

public class ServiceProvider : User
{
    public decimal Rating { get; set; }
    public bool Availability { get; set; }

    // Navigation Properties:
    public List<Schedule> Schedules { get; set; } = new();
    public List<Service> Services { get; set; } = new();
}
