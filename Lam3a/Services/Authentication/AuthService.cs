using System.Security.Cryptography;
using System.Text;

namespace Lam3a.Services.Authentication;

public static class AuthService
{
    private const int SaltSize = 32;
    private const int HashSize = 64;
    private const int Iterations = 100_000;

    private static readonly string Pepper =
        Environment.GetEnvironmentVariable("APP_PEPPER")
        ?? throw new InvalidOperationException("APP_PEPPER environment variable is not set");

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
}
