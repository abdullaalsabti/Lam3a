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
    }
}