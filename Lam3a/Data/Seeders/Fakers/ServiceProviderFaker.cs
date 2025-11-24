using Bogus;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Lam3a.Utils;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data.Seeders;

public sealed class ServiceProviderFaker : Faker<ServiceProvider>
{
    public ServiceProviderFaker()
    {
        UserFakerHelper.ApplyUserRules(this);

        RuleFor(p => p.Role, _ => Role.Provider);
        RuleFor(p => p.Rating, f => f.Random.Decimal(3, 5));
        RuleFor(p => p.Availability, f => f.Random.Bool());

        // Schedules and Services can be populated later in the seeder
        RuleFor(p => p.Schedules, _ => new List<Schedule>());
        RuleFor(p => p.Services, _ => new List<ProviderService>());
        RuleFor(p => p.FavoritedByClients, _ => new List<FavoriteProvider>());
    }
}
