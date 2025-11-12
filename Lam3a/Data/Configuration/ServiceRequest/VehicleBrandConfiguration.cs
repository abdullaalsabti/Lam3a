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
    }
}
