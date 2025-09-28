using Lam3a.Data.Entities;

namespace Lam3a.Data.ValueObjects;

public class Address
{
    public Guid AddressId { get; set; } = Guid.NewGuid();
    public string Landmark { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string Street { get; set; } = null!;
    public Coordinates MapCoordinates { get; set; } = new();

    // Navigation
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
