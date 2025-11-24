using Bogus;
using Lam3a.Data.Entities;

namespace Lam3a.Data.Seeders.Fakers;

public sealed class ClientFaker : Faker<Client>
{
    public ClientFaker()
    {
        UserFakerHelper.ApplyUserRules(this);
        //notifications
        RuleFor(
            c => c.Notifications,
            (f, c) =>
            {
                var notificationsFaker = new NotificationFaker();
                return notificationsFaker
                    .Generate(f.Random.Int(0, 3))
                    .Select(n =>
                    {
                        n.UserId = c.UserId;
                        return n;
                    })
                    .ToList();
            }
        );
    }
}
