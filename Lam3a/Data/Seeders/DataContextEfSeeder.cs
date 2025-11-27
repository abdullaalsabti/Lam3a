using Lam3a.Data.Entities;
using Lam3a.Data.Seeders.Fakers;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Data.Seeders;

public class DataContextEfSeeder
{
    //IServiceProvider: the DI container for creating, managing and injecting dependencies into constructors.
    public static async Task SeedAsync(IServiceProvider services)
    {
        //we need to create a scope an artificial scope like .addScoped in order to get DbContext.
        using var scope = services.CreateScope();
        //scope.ServiceProvider is the new DI container in the created scope that contains our DbContext service.
        var context = scope.ServiceProvider.GetRequiredService<DataContextEf>();
        var random = new Random();

        if (context.SeedStatus.Any())
            return;

        //seed clients
        var clientFaker = new ClientFaker();
        var clients = clientFaker.Generate(20);
        context.Clients.AddRange(clients);

        //seed vehicles
        var brandsWithModels = context.VehicleBrands.Include(b => b.Models).ToList();
        foreach (var client in clients)
        {
            var vehicleFaker = new VehicleFaker(client, brandsWithModels);
            var vehicles = vehicleFaker.Generate(random.Next(3) + 1); // 1–2 vehicles per client
            client.Vehicles.AddRange(vehicles);
            context.Vehicles.AddRange(vehicles);
        }

        //seed providers
        var serviceProvidersFaker = new ServiceProviderFaker();
        var serviceProviders = serviceProvidersFaker.Generate(20);
        context.ServiceProviders.AddRange(serviceProviders);

        //seed provider schedules
        var scheduleFaker = new ScheduleWithTimeSlotsFaker();
        foreach (var provider in serviceProviders)
        {
            var schedules = scheduleFaker.GenerateForProvider(provider);
            provider.Schedules.AddRange(schedules);
            context.Schedules.AddRange(schedules);
            context.TimeSlots.AddRange(schedules.SelectMany(sc => sc.TimeSlots));
        }

        //seed provider's offered services
        var categories = context.ServiceCategories.ToList();
        // var tags = context.ServiceTags.ToList();

        foreach (var provider in serviceProviders)
        {
            var providerServices = ProviderServiceFaker.GenerateForProvider(
                provider,
                categories
                // tags
            );
            context.ProviderServices.AddRange(providerServices);
        }

        context.SeedStatus.Add(new SeedStatus { Key = "Initial Seed" });
        await context.SaveChangesAsync();
    }
}
