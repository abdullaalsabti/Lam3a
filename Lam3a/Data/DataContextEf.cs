using System.Reflection;
using Lam3a.Data.Entities;
using Lam3a.Data.Seeders;
using Lam3a.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data;

public class DataContextEf : DbContext
{
    public DataContextEf(DbContextOptions<DataContextEf> options)
        : base(options) { }

    //User Related:
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ServiceProvider> ServiceProviders { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Address> Addresses { get; set; }

    //Schedule related:
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    //vehicle related
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<VehicleBrand> VehicleBrands { get; set; }

    //Services:
    public DbSet<ProviderService> ProviderServices { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    // public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<FavoriteProvider> FavoriteProviders { get; set; }

    //Seeding:
    public DbSet<SeedStatus> SeedStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
