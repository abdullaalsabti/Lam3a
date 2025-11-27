using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("ServiceCategories").HasKey(sc => sc.Id);

        builder.HasData(
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                Name = "Dry Clean",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                Name = "Exterior Wash",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                Name = "Interior Wash",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                Name = "Full Wash",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                Name = "Wax & Polish",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                Name = "Interior Detailing",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                Name = "Exterior Detailing",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                Name = "Headlight Restoration",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                Name = "Leather Seat Conditioning",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000a"),
                Name = "Odor Removal / Ozone Treatment",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000b"),
                Name = "Ceramic Coating",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000c"),
                Name = "Paint Protection Film (PPF)",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000d"),
                Name = "Engine Bay Cleaning",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000e"),
                Name = "Underbody Wash",
            },
            new ServiceCategory
            {
                Id = Guid.Parse("10000000-0000-0000-0000-00000000000f"),
                Name = "Tire & Rim Polishing",
            }
        );
    }
}
