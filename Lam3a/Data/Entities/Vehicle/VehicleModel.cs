namespace Lam3a.Data.Entities;

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
    public VehicleBrand Brand { get; set; }
}
