using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration;

public class ServiceRequestConfiguration : IEntityTypeConfiguration<ServiceRequest>
{
    public void Configure(EntityTypeBuilder<ServiceRequest> builder)
    {
        builder.ToTable("ServiceRequests").HasKey(rq => rq.Id);

        builder
            .HasOne(rq => rq.TimeSlot)
            .WithMany()
            .HasForeignKey(rq => rq.TimeSlotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(sr => sr.Service)
            .WithMany(s => s.ServiceRequests)
            .HasForeignKey(sr => sr.ServiceId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
