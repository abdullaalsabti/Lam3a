namespace Lam3a.Data.Entities;

public class RefreshToken
{
    public Guid TokenId { get; set; } = Guid.NewGuid();
    public required Guid UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAt { get; set; }
    public required bool IsRevoked { get; set; }
    public required bool IsUsed { get; set; }

    //NOTE:
    //The IsRevoked flag in your RefreshToken model is there to handle token invalidation before its natural expiration.
    // User logs out → You revoke their refresh token(s).
    // Password reset or security breach → All existing refresh tokens are revoked, forcing new logins.
    // Admin disables an account → Tokens can be revoked to immediately cut access.
}
