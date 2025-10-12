using Lam3a.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lam3a.Data.Configuration.ValueObjectsConfiguration;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules").HasKey(s => s.ScheduleId);

        builder.OwnsOne(
            s => s.TimeRange,
            tr =>
            {
                //can customize column names here later but no need now...s
            }
        );
    }
}
