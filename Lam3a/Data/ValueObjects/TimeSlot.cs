namespace Lam3a.Data.ValueObjects;

public class TimeSlot
{
    public Guid TimeSlotId { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = null!;
}
