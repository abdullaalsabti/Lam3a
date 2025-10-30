using System.Reflection;
using Lam3a.Data.Configuration;
using Lam3a.Data.Entities;
using Lam3a.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lam3a.Data;

public class DataContextEf : DbContext
{
    public DataContextEf(DbContextOptions<DataContextEf> options)
        : base(options) { }

    //User Related:
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Address> Addresses { get; set; }

    //Services:
    public DbSet<ProviderService> ProviderServices { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceTag> ServiceTags { get; set; }
    public DbSet<FavoriteProvider> FavoriteProviders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
