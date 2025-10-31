using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lam3a.Services.Authentication;

public static class AuthService
{
    private const int SaltSize = 32;
    private const int HashSize = 64;
    private const int Iterations = 100_000;
    private const int TokenSize = 64; // bytes
    private static readonly int JwtExpiryMinutes = 15;

    private static readonly string Pepper =
        Environment.GetEnvironmentVariable("APP_PEPPER")
        ?? throw new InvalidOperationException("APP_PEPPER environment variable is not set");

    private static readonly string JwtSecret =
        Environment.GetEnvironmentVariable("JWT_SECRET")
        ?? throw new InvalidOperationException("JWT_SECRET environment variable is not set");

    public static byte[] GeneratePasswordSalt()
    {
        var salt = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    public static byte[] GeneratePasswordHash(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password + Pepper);

        var passwordHash = Rfc2898DeriveBytes.Pbkdf2(
            passwordBytes,
            salt,
            Iterations,
            HashAlgorithmName.SHA512,
            HashSize
        );

        return passwordHash;
    }

    public static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        var hashToCheck = GeneratePasswordHash(password, salt);
        return CryptographicOperations.FixedTimeEquals(hash, hashToCheck);
    }

    public static string GenerateJwtToken(string userId, string email, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            "Lam3aAPI",
            "Lam3aClient",
            claims,
            expires: DateTime.UtcNow.AddMinutes(JwtExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        var randomBytes = new byte[TokenSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public static bool ValidateRefreshToken(string providedToken, string storedToken)
    {
        // Use constant-time comparison to avoid timing attacks
        var providedBytes = Convert.FromBase64String(providedToken);
        var storedBytes = Convert.FromBase64String(storedToken);
        return CryptographicOperations.FixedTimeEquals(providedBytes, storedBytes);
    }
}
