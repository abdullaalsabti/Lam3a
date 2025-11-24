using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class ProviderServiceConfiguration : IEntityTypeConfiguration<ProviderService>
{
    public void Configure(EntityTypeBuilder<ProviderService> builder)
    {
        builder.ToTable("ProviderServices").HasKey(s => s.Id);

        //Relationships:
        builder
            .HasOne(s => s.ServiceProvider)
            .WithMany(sp => sp.Services)
            .HasForeignKey(s => s.UserId);

        builder
            .HasOne(s => s.ServiceCategory)
            .WithMany(sc => sc.Services)
            .HasForeignKey(s => s.ServiceCategoryId);

        // builder.HasMany(s => s.ServiceTags).WithMany(st => st.Services);
    }
}
