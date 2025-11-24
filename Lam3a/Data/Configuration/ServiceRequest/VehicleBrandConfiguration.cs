using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class VehicleBrandConfiguration : IEntityTypeConfiguration<VehicleBrand>
{
    public void Configure(EntityTypeBuilder<VehicleBrand> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Name).IsRequired();
        
        builder.HasMany(b => b.Models)   // b is Brand
            .WithOne(m => m.Brand)    // m is VehicleModel
            .HasForeignKey(m => m.BrandId); // <-- foreign key lives in VehicleModel

        builder.HasData(
            new VehicleBrand { Id = 1, Name = "Toyota" },
            new VehicleBrand { Id = 2, Name = "Honda" },
            new VehicleBrand { Id = 3, Name = "Nissan" },
            new VehicleBrand { Id = 4, Name = "Hyundai" },
            new VehicleBrand { Id = 5, Name = "Kia" },
            new VehicleBrand { Id = 6, Name = "Ford" },
            new VehicleBrand { Id = 7, Name = "Chevrolet" },
            new VehicleBrand { Id = 8, Name = "Mercedes-Benz" },
            new VehicleBrand { Id = 9, Name = "BMW" },
            new VehicleBrand { Id = 10, Name = "Lexus" },
            new VehicleBrand { Id = 11, Name = "Audi" },
            new VehicleBrand { Id = 12, Name = "Volkswagen" },
            new VehicleBrand { Id = 13, Name = "Porsche" },
            new VehicleBrand { Id = 14, Name = "Land Rover" },
            new VehicleBrand { Id = 15, Name = "Jaguar" },
            new VehicleBrand { Id = 16, Name = "Mazda" },
            new VehicleBrand { Id = 17, Name = "Subaru" },
            new VehicleBrand { Id = 18, Name = "Mitsubishi" },
            new VehicleBrand { Id = 19, Name = "GMC" },
            new VehicleBrand { Id = 20, Name = "Cadillac" }
        );
    }
}
