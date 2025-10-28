namespace Lam3a.Data.Entities;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Primary key
    public required string Message { get; set; }
    public bool Read { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //Navigation Properties:
    public Guid UserId { get; set; } // Reference to the user
    public User User { get; set; } = null!;
}
