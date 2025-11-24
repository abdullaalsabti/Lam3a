using Bogus;
using Lam3a.Data.ValueObjects;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data.Seeders.Fakers;

public class ScheduleWithTimeSlotsFaker : Faker<Schedule>
{
    public List<Schedule> GenerateForProvider(ServiceProvider provider)
    {
        var schedules = new List<Schedule>();
        var days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        var random = new Random();

        foreach (var day in days)
        {
            var schedule = new Schedule
            {
                ScheduleId = Guid.NewGuid(),
                Day = day,
                ServiceProviderId = provider.UserId,
                ServiceProvider = provider,
            };

            // Generate time slots sequentially
            var slots = new List<TimeSlot>();

            // Random start hour between 8 (8:00 AM) and 12 (12:00 PM)
            var currentStartHour = random.Next(8, 13);
            var currentStartTime = TimeSpan.FromHours(currentStartHour);

            // Generate 1 to 3 slots per day
            var numberOfSlots = random.Next(1, 4);

            for (var i = 0; i < numberOfSlots; i++)
            {
                // Slot duration: 1 to 3 hours
                var durationHours = random.Next(1, 4);
                var endTime = currentStartTime.Add(TimeSpan.FromHours(durationHours));

                // Ensure we don't go past 10 PM (22:00)
                if (endTime.TotalHours > 22)
                    break;

                var timeSlot = new TimeSlot
                {
                    TimeSlotId = Guid.NewGuid(),
                    Start = currentStartTime,
                    End = endTime,
                    ScheduleId = schedule.ScheduleId,
                    Schedule = schedule,
                };

                slots.Add(timeSlot);

                // Break between slots: 1 to 2 hours
                var breakHours = random.Next(1, 3);
                currentStartTime = endTime.Add(TimeSpan.FromHours(breakHours));
            }

            schedule.TimeSlots.AddRange(slots);

            schedules.Add(schedule);
        }

        return schedules;
    }
}
