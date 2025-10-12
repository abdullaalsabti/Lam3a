using Lam3a.Data.ValueObjects;
using Lam3a.Utils;

namespace Lam3a.Data.Entities;

public class ServiceRequest
{
    public Guid Id { get; set; }
    public TimeRange TimeRange { get; set; } = new();
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public ServiceStatus Status { get; set; }

    //Navigation Properties:
    public Address Address { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; }
}
