using System.Security.Cryptography;
using Bogus;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Lam3a.Services.Authentication;
using Lam3a.Utils;

namespace Lam3a.Data.Seeders;

public static class UserFakerHelper
{
    private static readonly string Password = "Password123";
    private static readonly byte[] Salt = new byte[32];

    static UserFakerHelper()
    {
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(Salt);
    }

    public static void ApplyUserRules<T>(Faker<T> faker)
        where T : User
    {
        var hash = AuthService.GeneratePasswordHash(Password, Salt);

        faker.RuleFor(c => c.UserId, _ => Guid.NewGuid());

        faker.RuleFor(c => c.FirstName, f => f.Name.FirstName());
        faker.RuleFor(c => c.LastName, f => f.Name.LastName());
        faker.RuleFor(c => c.Email, f => f.Internet.Email());

        faker.RuleFor(c => c.PasswordHash, _ => hash);
        faker.RuleFor(c => c.PasswordSalt, Salt);

        faker.RuleFor(c => c.Role, _ => Role.Client);
        faker.RuleFor(c => c.UserAccountStatus, _ => UserAccountStatus.Verified);

        faker.RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+962#########"));
        faker.RuleFor(
            c => c.DateOfBirth,
            f => f.Date.Past(50, DateTime.Today.AddYears(-18)).ToUniversalTime()
        );
        faker.RuleFor(c => c.Gender, f => f.PickRandom<Gender>());

        //address
        faker.RuleFor(
            c => c.Address,
            (f, c) =>
                new Address
                {
                    UserId = c.UserId,
                    BuildingNumber = f.Address.BuildingNumber(),
                    Street = f.Address.StreetName(),
                    Landmark = f.Address.SecondaryAddress(),
                    MapCoordinates = new Coordinates
                    {
                        Latitude = (decimal)f.Address.Latitude(),
                        Longitude = (decimal)f.Address.Longitude(),
                    },
                }
        );
    }
}
