using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data.ValueObjects;

public class Schedule
{
    public Guid ScheduleId { get; set; } = Guid.NewGuid();
    public string Day { get; set; } = null!;

    // Navigation
    public List<TimeSlot> TimeSlots { get; set; } = new();
    public Guid ServiceProviderId { get; set; }
    public ServiceProvider ServiceProvider { get; set; } = null!;
}
