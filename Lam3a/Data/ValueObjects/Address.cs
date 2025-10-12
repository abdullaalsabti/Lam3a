using Lam3a.Data.Entities;

namespace Lam3a.Data.ValueObjects;

public class Address
{
    public Guid AddressId { get; set; } = Guid.NewGuid();
    public string Landmark { get; set; } = string.Empty;
    public required string BuildingNumber { get; set; }
    public required string Street { get; set; }

    // Navigation
    public Coordinates MapCoordinates { get; set; } = new();
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
