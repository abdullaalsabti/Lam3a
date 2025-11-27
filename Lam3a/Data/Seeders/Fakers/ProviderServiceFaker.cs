using Bogus;
using Lam3a.Data.Entities;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data.Seeders.Fakers;

public class ProviderServiceFaker
{
    public static List<ProviderService> GenerateForProvider(
        ServiceProvider provider,
        List<ServiceCategory> categories
        // List<ServiceTag> tags
    )
    {
        var faker = new Faker();
        var services = new List<ProviderService>();

        // Pick 1-3 random categories
        var selectedCategories = faker.PickRandom(categories, faker.Random.Int(1, 3)).ToList();

        foreach (var category in selectedCategories)
        {
            var service = new ProviderService
            {
                Id = Guid.NewGuid(),
                Price = faker.Random.Decimal(50, 500),
                Description = faker.Lorem.Sentence(),
                EstimatedTime = faker.PickRandom(30, 60, 90, 120),
                UserId = provider.UserId,
                CategoryId = category.Id,
            };
            services.Add(service);
        }

        return services;
    }
}
