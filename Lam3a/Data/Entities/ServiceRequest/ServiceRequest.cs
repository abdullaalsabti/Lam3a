using Lam3a.Data.ValueObjects;
using Lam3a.Utils;

namespace Lam3a.Data.Entities;

public class ServiceRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public String VehiclePlateNumber { get; set; }
    public TimeSlot TimeSlot { get; set; } = new();
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public ServiceStatus Status { get; set; }
    //FK
    public Guid ServiceProviderId { get; set; }  //the provider that will offer the service in the request
    public Guid ServiceId { get; set; } //service offered in the request (provider service)
    
    //Navigation Properties:
    public Address Address { get; set; }
    public Guid TimeSlotId { get; set; }
    public ServiceProvider provider { get; set; }
    public ProviderService Service { get; set; }
    public Vehicle Vehicle { get; set; }
}
