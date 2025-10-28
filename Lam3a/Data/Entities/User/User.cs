using Lam3a.Data.ValueObjects;
using Lam3a.Utils;

namespace Lam3a.Data.Entities;

public abstract class User
{
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required string Phone { get; set; }
    public required Gender Gender { get; set; } = Gender.Male;
    public required Role Role { get; set; } = Role.Client;

    public UserAccountStatus UserAccountStatus { get; set; } = UserAccountStatus.Unverified;

    //Navigation Properties:
    public List<Notification> Notifications { get; set; } = new();
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
}
