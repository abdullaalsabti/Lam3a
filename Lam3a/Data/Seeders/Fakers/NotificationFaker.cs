using Bogus;
using Lam3a.Data.Entities;

namespace Lam3a.Data.Seeders;

public sealed class NotificationFaker : Faker<Notification>
{
    public NotificationFaker()
    {
        RuleFor(n => n.Id, _ => Guid.NewGuid());
        RuleFor(n => n.Message, f => f.Lorem.Sentence(f.Random.Int(5, 10)));
        RuleFor(n => n.Read, f => f.Random.Bool());
        RuleFor(n => n.CreatedAt, f => f.Date.Recent(15).ToUniversalTime());
    }
}
