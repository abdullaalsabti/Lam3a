using Lam3a.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration.ValueObjectsConfiguration;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules").HasKey(s => s.ScheduleId);

        builder
            .HasOne(s => s.ServiceProvider)
            .WithMany(p => p.Schedules)
            .HasForeignKey(s => s.ServiceProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.TimeSlots)
            .WithOne(ts => ts.Schedule)
            .HasForeignKey(ts => ts.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
