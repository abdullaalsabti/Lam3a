using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("ServiceCategories").HasKey(sc => sc.CategoryId);

        builder.HasData(
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                CategoryName = "Dry Clean",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                CategoryName = "Exterior Wash",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                CategoryName = "Interior Wash",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                CategoryName = "Full Wash",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                CategoryName = "Wax & Polish",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                CategoryName = "Interior Detailing",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                CategoryName = "Exterior Detailing",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                CategoryName = "Headlight Restoration",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                CategoryName = "Leather Seat Conditioning",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000a"),
                CategoryName = "Odor Removal / Ozone Treatment",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000b"),
                CategoryName = "Ceramic Coating",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000c"),
                CategoryName = "Paint Protection Film (PPF)",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000d"),
                CategoryName = "Engine Bay Cleaning",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000e"),
                CategoryName = "Underbody Wash",
            },
            new ServiceCategory
            {
                CategoryId = Guid.Parse("10000000-0000-0000-0000-00000000000f"),
                CategoryName = "Tire & Rim Polishing",
            }
        );
    }
}
