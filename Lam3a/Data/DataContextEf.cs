using System.Reflection;
using Lam3a.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Data;

public class DataContextEf : DbContext
{
    public DataContextEf(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private string _connectionString { get; }

    public DbSet<Client> Clients { get; set; }
    public DbSet<ServiceProvider> ServiceProviders { get; set; }

    public DbSet<Service> Services { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
