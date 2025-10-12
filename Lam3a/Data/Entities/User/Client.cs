namespace Lam3a.Data.Entities;

public class Client : User
{
    //Navigation Properties:
    public List<Vehicle> Vehicles { get; set; } = new();
}
