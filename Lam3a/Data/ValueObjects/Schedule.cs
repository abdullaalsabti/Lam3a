using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data.ValueObjects;

public class Schedule
{
    public Guid ScheduleId { get; set; } = Guid.NewGuid();
    public string Day { get; set; } = null!;
    public TimeRange TimeRange { get; set; } = new();

    // Navigation
    public Guid ServiceProviderId { get; set; }
    public ServiceProvider ServiceProvider { get; set; } = null!;
}
