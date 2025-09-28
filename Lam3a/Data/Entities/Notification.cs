namespace Lam3a.Data.Entities;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Primary key
    public Guid UserId { get; set; } // Reference to the user
    public string Message { get; set; } = null!;
    public bool Read { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
