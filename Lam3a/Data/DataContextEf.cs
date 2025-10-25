using System.Reflection;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Data;

public class DataContextEf : DbContext
{
    public DataContextEf(DbContextOptions<DataContextEf> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
