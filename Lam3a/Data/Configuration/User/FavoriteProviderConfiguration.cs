using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration.User;

public class FavoriteProviderConfiguration : IEntityTypeConfiguration<FavoriteProvider>
{
    public void Configure(EntityTypeBuilder<FavoriteProvider> builder)
    {
        builder.HasKey(fp => new { fp.ClientId, fp.ServiceProviderId });

        builder
            .HasOne(fp => fp.Client)
            .WithMany(c => c.FavoriteProviders)
            .HasForeignKey(fp => fp.ClientId);

        builder
            .HasOne(fp => fp.ServiceProvider)
            .WithMany(sp => sp.FavoritedByClients)
            .HasForeignKey(fp => fp.ServiceProviderId);
    }
}
