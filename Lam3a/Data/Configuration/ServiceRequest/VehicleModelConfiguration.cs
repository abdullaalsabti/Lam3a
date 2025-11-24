using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
{
    public void Configure(EntityTypeBuilder<VehicleModel> builder)
    {
        builder.HasKey(v => v.Id);
        
        builder
            .HasOne(v => v.Brand)
            .WithMany(b => b.Models)
            .HasForeignKey(b => b.BrandId);

        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasData(
            // Toyota (Id: 1)
            new VehicleModel { Id = 1, BrandId = 1, Name = "Camry" },
            new VehicleModel { Id = 2, BrandId = 1, Name = "Corolla" },
            new VehicleModel { Id = 3, BrandId = 1, Name = "Land Cruiser" },
            new VehicleModel { Id = 4, BrandId = 1, Name = "Crown" },
            new VehicleModel { Id = 5, BrandId = 1, Name = "Hilux" },
            new VehicleModel { Id = 6, BrandId = 1, Name = "RAV4" },
            new VehicleModel { Id = 7, BrandId = 1, Name = "Highlander" },

            // Honda (Id: 2)
            new VehicleModel { Id = 8, BrandId = 2, Name = "Civic" },
            new VehicleModel { Id = 9, BrandId = 2, Name = "Accord" },
            new VehicleModel { Id = 10, BrandId = 2, Name = "CR-V" },
            new VehicleModel { Id = 11, BrandId = 2, Name = "Pilot" },
            new VehicleModel { Id = 12, BrandId = 2, Name = "City" },

            // Nissan (Id: 3)
            new VehicleModel { Id = 13, BrandId = 3, Name = "Sunny" },
            new VehicleModel { Id = 14, BrandId = 3, Name = "Sentra" },
            new VehicleModel { Id = 15, BrandId = 3, Name = "Altima" },
            new VehicleModel { Id = 16, BrandId = 3, Name = "Patrol" },
            new VehicleModel { Id = 17, BrandId = 3, Name = "X-Trail" },

            // Hyundai (Id: 4)
            new VehicleModel { Id = 18, BrandId = 4, Name = "Accent" },
            new VehicleModel { Id = 19, BrandId = 4, Name = "Elantra" },
            new VehicleModel { Id = 20, BrandId = 4, Name = "Sonata" },
            new VehicleModel { Id = 21, BrandId = 4, Name = "Tucson" },
            new VehicleModel { Id = 22, BrandId = 4, Name = "Santa Fe" },

            // Kia (Id: 5)
            new VehicleModel { Id = 23, BrandId = 5, Name = "Rio" },
            new VehicleModel { Id = 24, BrandId = 5, Name = "Cerato" },
            new VehicleModel { Id = 25, BrandId = 5, Name = "K5" },
            new VehicleModel { Id = 26, BrandId = 5, Name = "Sportage" },
            new VehicleModel { Id = 27, BrandId = 5, Name = "Sorento" },

            // Ford (Id: 6)
            new VehicleModel { Id = 28, BrandId = 6, Name = "Taurus" },
            new VehicleModel { Id = 29, BrandId = 6, Name = "Mustang" },
            new VehicleModel { Id = 30, BrandId = 6, Name = "Explorer" },
            new VehicleModel { Id = 31, BrandId = 6, Name = "F-150" },
            new VehicleModel { Id = 32, BrandId = 6, Name = "Expedition" },

            // Chevrolet (Id: 7)
            new VehicleModel { Id = 33, BrandId = 7, Name = "Spark" },
            new VehicleModel { Id = 34, BrandId = 7, Name = "Malibu" },
            new VehicleModel { Id = 35, BrandId = 7, Name = "Camaro" },
            new VehicleModel { Id = 36, BrandId = 7, Name = "Tahoe" },
            new VehicleModel { Id = 37, BrandId = 7, Name = "Silverado" },

            // Mercedes-Benz (Id: 8)
            new VehicleModel { Id = 38, BrandId = 8, Name = "C-Class" },
            new VehicleModel { Id = 39, BrandId = 8, Name = "E-Class" },
            new VehicleModel { Id = 40, BrandId = 8, Name = "S-Class" },
            new VehicleModel { Id = 41, BrandId = 8, Name = "G-Class" },

            // BMW (Id: 9)
            new VehicleModel { Id = 42, BrandId = 9, Name = "3 Series" },
            new VehicleModel { Id = 43, BrandId = 9, Name = "5 Series" },
            new VehicleModel { Id = 44, BrandId = 9, Name = "7 Series" },
            new VehicleModel { Id = 45, BrandId = 9, Name = "X5" },

            // Lexus (Id: 10)
            new VehicleModel { Id = 46, BrandId = 10, Name = "IS" },
            new VehicleModel { Id = 47, BrandId = 10, Name = "ES" },
            new VehicleModel { Id = 48, BrandId = 10, Name = "LS" },
            new VehicleModel { Id = 49, BrandId = 10, Name = "LX" },
            new VehicleModel { Id = 50, BrandId = 10, Name = "RX" },

            // Audi (Id: 11)
            new VehicleModel { Id = 51, BrandId = 11, Name = "A3" },
            new VehicleModel { Id = 52, BrandId = 11, Name = "A4" },
            new VehicleModel { Id = 53, BrandId = 11, Name = "A6" },
            new VehicleModel { Id = 54, BrandId = 11, Name = "Q5" },
            new VehicleModel { Id = 55, BrandId = 11, Name = "Q7" },

            // Volkswagen (Id: 12)
            new VehicleModel { Id = 56, BrandId = 12, Name = "Golf" },
            new VehicleModel { Id = 57, BrandId = 12, Name = "Jetta" },
            new VehicleModel { Id = 58, BrandId = 12, Name = "Passat" },
            new VehicleModel { Id = 59, BrandId = 12, Name = "Tiguan" },
            new VehicleModel { Id = 60, BrandId = 12, Name = "Touareg" },

            // Porsche (Id: 13)
            new VehicleModel { Id = 61, BrandId = 13, Name = "911" },
            new VehicleModel { Id = 62, BrandId = 13, Name = "Cayenne" },
            new VehicleModel { Id = 63, BrandId = 13, Name = "Macan" },
            new VehicleModel { Id = 64, BrandId = 13, Name = "Panamera" },
            new VehicleModel { Id = 65, BrandId = 13, Name = "Taycan" },

            // Land Rover (Id: 14)
            new VehicleModel { Id = 66, BrandId = 14, Name = "Range Rover" },
            new VehicleModel { Id = 67, BrandId = 14, Name = "Defender" },
            new VehicleModel { Id = 68, BrandId = 14, Name = "Discovery" },
            new VehicleModel { Id = 69, BrandId = 14, Name = "Evoque" },
            new VehicleModel { Id = 70, BrandId = 14, Name = "Velar" },

            // Jaguar (Id: 15)
            new VehicleModel { Id = 71, BrandId = 15, Name = "XE" },
            new VehicleModel { Id = 72, BrandId = 15, Name = "XF" },
            new VehicleModel { Id = 73, BrandId = 15, Name = "F-Pace" },
            new VehicleModel { Id = 74, BrandId = 15, Name = "E-Pace" },
            new VehicleModel { Id = 75, BrandId = 15, Name = "F-Type" },

            // Mazda (Id: 16)
            new VehicleModel { Id = 76, BrandId = 16, Name = "Mazda3" },
            new VehicleModel { Id = 77, BrandId = 16, Name = "Mazda6" },
            new VehicleModel { Id = 78, BrandId = 16, Name = "CX-5" },
            new VehicleModel { Id = 79, BrandId = 16, Name = "CX-9" },
            new VehicleModel { Id = 80, BrandId = 16, Name = "CX-30" },

            // Subaru (Id: 17)
            new VehicleModel { Id = 81, BrandId = 17, Name = "Impreza" },
            new VehicleModel { Id = 82, BrandId = 17, Name = "Legacy" },
            new VehicleModel { Id = 83, BrandId = 17, Name = "Outback" },
            new VehicleModel { Id = 84, BrandId = 17, Name = "Forester" },
            new VehicleModel { Id = 85, BrandId = 17, Name = "Crosstrek" },

            // Mitsubishi (Id: 18)
            new VehicleModel { Id = 86, BrandId = 18, Name = "Lancer" },
            new VehicleModel { Id = 87, BrandId = 18, Name = "Pajero" },
            new VehicleModel { Id = 88, BrandId = 18, Name = "Outlander" },
            new VehicleModel { Id = 89, BrandId = 18, Name = "ASX" },
            new VehicleModel { Id = 90, BrandId = 18, Name = "Eclipse Cross" },

            // GMC (Id: 19)
            new VehicleModel { Id = 91, BrandId = 19, Name = "Sierra" },
            new VehicleModel { Id = 92, BrandId = 19, Name = "Yukon" },
            new VehicleModel { Id = 93, BrandId = 19, Name = "Terrain" },
            new VehicleModel { Id = 94, BrandId = 19, Name = "Acadia" },
            new VehicleModel { Id = 95, BrandId = 19, Name = "Canyon" },

            // Cadillac (Id: 20)
            new VehicleModel { Id = 96, BrandId = 20, Name = "Escalade" },
            new VehicleModel { Id = 97, BrandId = 20, Name = "CT5" },
            new VehicleModel { Id = 98, BrandId = 20, Name = "XT5" },
            new VehicleModel { Id = 99, BrandId = 20, Name = "XT6" },
            new VehicleModel { Id = 100, BrandId = 20, Name = "CT4" }
        );
    }
}