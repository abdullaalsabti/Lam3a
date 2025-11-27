// using Lam3a.Data.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Lam3a.Data.Configuration;
//
// public class ServiceTagsConfiguration : IEntityTypeConfiguration<ServiceTag>
// {
//     public void Configure(EntityTypeBuilder<ServiceTag> builder)
//     {
//         builder.ToTable("ServiceTags").HasKey(sr => sr.TagId);
//         builder.HasData(
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
//                 TagName = "Premium",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
//                 TagName = "Eco-Friendly",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111113"),
//                 TagName = "Affordable",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111114"),
//                 TagName = "Fast Service",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111115"),
//                 TagName = "Phone Support",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111116"),
//                 TagName = "24/7 Availability",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111117"),
//                 TagName = "Waterless",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111118"),
//                 TagName = "Interior Specialist",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111119"),
//                 TagName = "Exterior Specialist",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111120"),
//                 TagName = "Luxury Cars",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111121"),
//                 TagName = "Pickup & Drop-off",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111122"),
//                 TagName = "Pet Hair Removal",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111123"),
//                 TagName = "Family Friendly",
//             },
//             new ServiceTag
//             {
//                 TagId = Guid.Parse("11111111-1111-1111-1111-111111111124"),
//                 TagName = "Contactless Payment",
//             }
//         );
//     }
// }
