using Bogus;
using Lam3a.Data.Entities;
using Lam3a.Utils;

namespace Lam3a.Data.Seeders.Fakers;

public sealed class VehicleFaker : Faker<Vehicle>
{
    public VehicleFaker(Client client, List<VehicleBrand> brandsWithModels)
    {
        CustomInstantiator(f =>
        {
            var brand = f.PickRandom(brandsWithModels);
            var model = f.PickRandom(brand.Models);

            return new Vehicle(
                f.Vehicle.Vin().Substring(0, 7),
                brand.Id,
                model.Id,
                f.PickRandom<VehicleColor>(),
                f.PickRandom<CarType>(),
                client.UserId
            )
            {
                Brand = brand,
                Model = model,
            };
        });
    }
}
