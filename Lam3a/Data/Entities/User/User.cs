using Lam3a.Data.ValueObjects;

namespace Lam3a.Data.Entities;

public abstract class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = null!;
    public string Role { get; set; } = null!;
    public Address Address { get; set; } = new();
}
