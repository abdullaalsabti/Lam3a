using Lam3a.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicle");
        builder.HasKey(v => v.PlateNumber);
        builder.Property(v => v.PlateNumber).HasMaxLength(8);

        // Relationships
        builder.HasOne(v => v.Brand)
            .WithMany()
            .HasForeignKey(v => v.BrandId);
           

        builder.HasOne(v => v.Model)
            .WithMany() // VehicleModel doesn’t have a Vehicles collection, so leave it empty
            .HasForeignKey(v => v.ModelId);
    
    }
}
